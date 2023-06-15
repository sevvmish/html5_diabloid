using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator mainAnimator;
    private PlayerManager playerManager;
    private int animatorPriority;

    private void Update()
    {
        //animator data about run-idle
        if (playerManager.playerRigidbody.velocity.magnitude > 0.1f)
        {
            RunAnimation();
        }
        else
        {
            IdleAnimation();
        }
    }

    //public bool isHitting { get; private set; }

    public void SetData(Animator animator, PlayerManager manager)
    {
        mainAnimator = animator;
        playerManager = manager;
    }

    public void IdleAnimation()
    {
        if (!playerManager.mainPlayerEntity.IsPlayerCanMove) return;

        if (animatorPriority != 0)
        {
            if (animatorPriority == 1)
            {
                mainAnimator.SetTrigger("idle");
            }
            else
            {
                mainAnimator.Play("Idle");
            }
            
            //
            animatorPriority = 0;
        }
    }



    public void DamageImpactAnimation()
    {
        if (playerManager.mainPlayerEntity.IsHitting) return;

        mainAnimator.Play("DamageImpact");
    }

    public void RunAnimation()
    {
        if (!playerManager.mainPlayerEntity.IsPlayerCanMove) return;

        if (animatorPriority != 1)
        {
            if (animatorPriority == 0)
            {
                mainAnimator.SetTrigger("run");
            }
            else
            {
                mainAnimator.Play("Run");
            }
            
            //
            animatorPriority = 1;
        }
    }

    public void HitAnimation()
    {        
        StartCoroutine(playHit());
    }
    private IEnumerator playHit()
    {
        //isHitting = true;
        mainAnimator.StopPlayback();
        mainAnimator.Play("Hit1h_right");
        animatorPriority = 2;

        yield return new WaitForSeconds(0.4f);
        IdleAnimation();
        //isHitting = false;
        //animatorPriority = 0;
    }
}

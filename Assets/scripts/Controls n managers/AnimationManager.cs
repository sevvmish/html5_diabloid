using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator mainAnimator;
    private Creature player;
    private int animatorPriority;

    private void Update()
    {        
        //animator data about run-idle
        if (player.PlayerRigidbody.velocity.magnitude > 0.1f)
        {
            RunAnimation();
        }
        else
        {
            IdleAnimation();
        }
    }

    public void SetData(Animator animator, Creature _player)
    {
        mainAnimator = animator;
        player = _player;
    }

    public void IdleAnimation()
    {
        if (!player.IsPlayerCanMove) return;

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
        
        if (player.IsHitting) return;
        
        switch(UnityEngine.Random.Range(0,3))
        {
            case 0:
                mainAnimator.Play("DamageImpact");
                break;
            case 1:
                mainAnimator.Play("DamageImpact 0");
                break;
            case 2:
                mainAnimator.Play("DamageImpact 1");
                break;
        }

        
    }

    public void RunAnimation()
    {
        if (!player.IsPlayerCanMove) return;

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
        mainAnimator.StopPlayback();
        mainAnimator.Play("Hit1h_right");
        animatorPriority = 2;

        yield return new WaitForSeconds(0.4f);
        IdleAnimation();
    }
}

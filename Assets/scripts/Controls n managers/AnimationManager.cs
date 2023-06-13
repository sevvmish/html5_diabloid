using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator mainAnimator;
    private PlayerManager playerManager;
    private int animatorPriority;

    public bool isHitting { get; private set; }

    public void SetData(Animator animator, PlayerManager manager)
    {
        mainAnimator = animator;
        playerManager = manager;
    }

    public void IdleAnimation()
    {
        if (isHitting) return;

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

    public void RunAnimation()
    {
        if (isHitting) return;

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
        if (isHitting) return;
        StartCoroutine(playHit());
    }
    private IEnumerator playHit()
    {
        isHitting = true;
        
        mainAnimator.Play("Hit1h");
        animatorPriority = 2;

        yield return new WaitForSeconds(0.5f);
        isHitting = false;
        
    }
}

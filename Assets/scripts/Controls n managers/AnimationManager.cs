using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator mainAnimator;
    private Creature player;
    private int animatorPriority;
    private Action updateMoveIdleAnimation;
    private float _timer;

    private void updateMoveIdleByRigidbody()
    {
        if (player.PlayerRigidbody.velocity.magnitude > 0.1f)
        {
            RunAnimation();
        }
        else
        {
            IdleAnimation();
        }
    }

    private void updateMoveIdleByNavmeshAgent()
    {
        if (player.PlayerAgent.velocity.magnitude > 0.1f)
        {
            RunAnimation();
        }
        else
        {
            IdleAnimation();
        }
    }

    private void Update()
    {
        if (_timer>0.1f)
        {
            _timer = 0;
            updateMoveIdleAnimation?.Invoke();
        }
        else
        {
            _timer += Time.deltaTime;
        }
        

        /*
        //animator data about run-idle
        if (player.PlayerRigidbody != null)
        {
            if (player.PlayerRigidbody.velocity.magnitude > 0.1f)
            {
                RunAnimation();
            }
            else
            {
                IdleAnimation();
            }
        }

        if (player.PlayerAgent != null)
        {
            if (player.PlayerAgent.velocity.magnitude > 0.1f)
            {
                RunAnimation();
            }
            else
            {
                IdleAnimation();
            }
        }*/
    }

    public void SetData(Animator animator, Creature _player)
    {
        mainAnimator = animator;
        player = _player;
        if (player.PlayerRigidbody != null)
        {
            updateMoveIdleAnimation = updateMoveIdleByRigidbody;
        }
        else
        {
            updateMoveIdleAnimation = updateMoveIdleByNavmeshAgent;
        }
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
        
        if (player.IsPerformingSkill) return;
        
        if (animatorPriority<1)
        {
            switch (UnityEngine.Random.Range(0, 3))
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

            animatorPriority = 2;
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

    public void Hit1HAnimation()
    {        
        StartCoroutine(playHit());
    }
    private IEnumerator playHit()
    {
        mainAnimator.StopPlayback();
        mainAnimator.Play("Hit1h_right");
        animatorPriority = 3;

        yield return new WaitForSeconds(0.5f);
        IdleAnimation();
    }
}

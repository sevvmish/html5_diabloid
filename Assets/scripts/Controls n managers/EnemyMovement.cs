using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{    
    private Creature enemy;
    private EnemyManager enemyManager;
    private float _timer;
    private IHitable aimHitable;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyManager>().EnemyEntity;
        enemyManager = GetComponent<EnemyManager>();
        aimHitable = GameManager.Instance.mainPlayerTransform.GetComponent<IHitable>();
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (_timer>0.1f)
        {
            _timer = 0;
            float distance = (transform.position - aimHitable.AimTransform.position).magnitude;

            if (distance <= 1.5f)
            {
                enemyManager.SkillOneAttack(aimHitable);                
            }
            else
            {
                movementToPoint(Vector3.Lerp(transform.position, aimHitable.AimTransform.position, (distance - 1)/distance));
            }
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    private void movementToPoint(Vector3 point)
    {        
        agent.speed = enemy.CurrentSpeed;
        agent.SetDestination(point);
    }
}

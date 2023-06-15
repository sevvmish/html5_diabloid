using DG.Tweening;
using UnityEngine;

public class WeaponTriggerMelee : MonoBehaviour
{
    private int ownerID;
    private int enemyLimit;
    private int currentEnemyLimit;
    private DamageOutput weaponDamage;
    private Transform player;
    private float distance;
    private CreatureSides aimEnemies;

    private void OnEnable()
    {
        currentEnemyLimit = 0;
    }

    
    private void OnTriggerEnter(Collider other)
    {                
        if (other != null)
        {
            IHitable h = other.GetComponent<IHitable>();

            if (h != null && h.OwnerID != ownerID /*&& h.CreatureSide == aimEnemies*/)
            {                
                player.DOLookAt(new Vector3(h.AimTransform.position.x, 0, h.AimTransform.position.z), 0.1f);
                h.ReceiveHit(weaponDamage);
                currentEnemyLimit++;

                if (currentEnemyLimit >= enemyLimit) 
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }


    public void SetBaseConditions(int ownerID, Transform player, CreatureSides side)
    {       
        this.ownerID = ownerID;
        this.player = player;

        if (side == CreatureSides.AllGood)
        {
            aimEnemies = CreatureSides.AllBad;
        }
        else if (side == CreatureSides.AllBad)
        {
            aimEnemies = CreatureSides.AllGood;
        }
    }

    public void UpdateConditions(float distance, int enemiesLimit, DamageOutput damage)
    {
        this.distance = distance;
        enemyLimit = enemiesLimit;
        weaponDamage = damage;

        transform.localScale = Vector3.one * distance * 2;
    }        
}

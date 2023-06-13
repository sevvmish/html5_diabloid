using DG.Tweening;
using UnityEngine;

public class WeaponTriggerMelee : MonoBehaviour
{
    private int ownerID;
    private int enemyLimit;
    private int currentEnemyLimit;
    private WeaponDamage weaponDamage;
    private Transform player;

    private void OnEnable()
    {
        currentEnemyLimit = 0;
    }

    
    private void OnTriggerEnter(Collider other)
    {                
        if (other != null)
        {
            IHitable h = other.GetComponent<IHitable>();

            if (h != null && h.OwnerID != ownerID)
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


    public void SetConditions(int ownerID, int enemyLimit, WeaponDamage weaponDamage, Transform player)
    {
        this.weaponDamage = weaponDamage;
        this.ownerID = ownerID;
        this.enemyLimit = enemyLimit;
        this.player = player;
    }

}

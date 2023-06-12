using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    private PlayerManager owner;
    private int enemyLimit;
    private int currentEnemyLimit;
    private WeaponDamage weaponDamage;

    private void OnEnable()
    {
        currentEnemyLimit = 0;
    }

    private void OnTriggerEnter(Collider other)
    {                
        if (other != null)
        {
            IHitable h = other.GetComponent<IHitable>();

            if (h != null && h.owner != owner)
            {
                owner.transform.DOLookAt(new Vector3(h.owner.transform.position.x, 0, h.owner.transform.position.z), 0.1f);
                h.ReceiveHit(weaponDamage);
                currentEnemyLimit++;

                if (currentEnemyLimit >= enemyLimit) 
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetConditions(PlayerManager owner, int enemyLimit, WeaponDamage weaponDamage)
    {
        this.weaponDamage = weaponDamage;
        this.owner = owner;
        this.enemyLimit = enemyLimit;
    }

}

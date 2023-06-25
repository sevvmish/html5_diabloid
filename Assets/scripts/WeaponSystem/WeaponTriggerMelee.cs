using DG.Tweening;
using System.Collections;
using UnityEngine;

public class WeaponTriggerMelee : MonoBehaviour
{
    private int ownerID;
    private int enemyLimit;
    private int currentEnemyLimit;
    private DamageOutput weaponDamage;
    private Transform player;
    private float defaultAngle;
    private int playerSide;

    private IHitable aim;
    private float angle;

    private void OnEnable()
    {
        defaultAngle = 60;
        currentEnemyLimit = 0;
    }

    
    private void OnTriggerEnter(Collider other)
    {                
        if (other != null)
        {
            aim = other.GetComponent<IHitable>();
            
            if (aim != null && aim.OwnerID != ownerID && aim.CreatureSide != playerSide)
            {                
                StartCoroutine(playHit(aim));
            }
        }
    }

    private IEnumerator playHit(IHitable aim)
    {   
        player.DOLookAt(new Vector3(aim.AimTransform.position.x, 0, aim.AimTransform.position.z), 0.1f);        
        yield return new WaitForSeconds(0.1f);

        angle = Vector3.Angle(player.forward, (aim.AimTransform.position - player.position));

        if (angle < defaultAngle)
        {
            aim.ReceiveHit(weaponDamage);
            currentEnemyLimit++;

            if (currentEnemyLimit >= enemyLimit)
            {
                gameObject.SetActive(false);
            }
        }
    }


    public void SetBaseConditions(int ownerID, Transform player, int side)
    {       
        this.ownerID = ownerID;
        this.player = player;

        playerSide = side;
    }

    public void UpdateConditions(float distance, int enemiesLimit, DamageOutput damage)
    {        
        enemyLimit = enemiesLimit;
        weaponDamage = damage;

        transform.localScale = Vector3.one * distance * 2;
    }        
}

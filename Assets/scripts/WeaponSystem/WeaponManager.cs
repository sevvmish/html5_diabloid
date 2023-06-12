using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private GameObject weaponTriggerZone;
    private bool isCooldownActive;
    private Creature playerEntity;
    public void SetPlayerData(Creature creature) => playerEntity = creature;
    
    // Start is called before the first frame update
    void Start()
    {
        weaponTriggerZone = createMeleeTrigger();
        weaponTriggerZone.SetActive(false);
    }

    public bool Attack()
    {        
        if (!isCooldownActive)
        {
            StartCoroutine(attack());
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool Attack(IHitable aim)
    {
        if ((transform.position - aim.owner.transform.position).magnitude <= (1 + aim.PlayerRadius) && !isCooldownActive)
        {
            StartCoroutine(attack());
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator attack()
    {
        isCooldownActive = true;
        weaponTriggerZone.SetActive(true);
        yield return new WaitForSeconds(Time.fixedDeltaTime);
        weaponTriggerZone.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        isCooldownActive = false;
    }
        
    private GameObject createMeleeTrigger()
    {        
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        g.transform.parent = transform;
        g.GetComponent<SphereCollider>().isTrigger = true;
        g.GetComponent<MeshRenderer>().enabled = false;
        Destroy(g.GetComponent<MeshFilter>());
        g.transform.localScale = Vector3.one * 3;
        g.transform.localPosition = new Vector3(0, 0.8f, 0);

        WeaponDamage wd = new WeaponDamage();
        wd.SetWeaponDamage(DamageDistanceTypes.melee, DamageTypes.melee, 5, 10, 1.5f);
        g.AddComponent<WeaponTrigger>().SetConditions(GetComponent<PlayerManager>(), 1, wd);
        
        return g;
    }
}

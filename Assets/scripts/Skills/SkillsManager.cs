using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillsManager : MonoBehaviour
{
    public static Skill GetSkillById(int id)
    {
        switch(id)
        {
            case 1:
                return null;
        }

        throw new NotImplementedException();
    }

}

public abstract class Skill : MonoBehaviour
{
    public float Distance { get; set; }
    public float Cooldown { get; set; }
    public string SkillName { get; set; }
    public string SkillDescription { get; set; }
    public DamageDistanceTypes DamageDistanceType { get; set; }
    public Weapon MainWeapon { get; set; }
    public Weapon SecondWeapon { get;  set; }
    public Creature PlayerData { get;  set; }
    public Action InvokeAnimation { get;  set; }
    public Action<bool> InvokeSkillHitStatus { get; set; }
    public Transform mainPlayerTransform { get;  set; }
    public bool IsCooldownActive { get;  set; }
    public void SetCooldown(bool isCooldown) { IsCooldownActive = isCooldown;}
    public GameObject WeaponTrigger { get;  set; }

    public Skill() { }

    public void createWeaponTrigger()
    {
        MainWeapon = PlayerData.MainInventory.MainWeapon;
        SecondWeapon = PlayerData.MainInventory.SecondWeapon;

        WeaponTrigger = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        WeaponTrigger.transform.parent = mainPlayerTransform;
        WeaponTrigger.GetComponent<SphereCollider>().isTrigger = true;
        WeaponTrigger.GetComponent<MeshRenderer>().enabled = false;
        Destroy(WeaponTrigger.GetComponent<MeshFilter>());
        WeaponTrigger.transform.localScale = Vector3.one * Distance * 2;
        WeaponTrigger.transform.localPosition = new Vector3(0, 0.5f, 0);

        WeaponDamage wd = MainWeapon.WeaponDamage;
        WeaponTrigger.AddComponent<WeaponTriggerMelee>().SetConditions(PlayerData.OwnerID, 1, wd, mainPlayerTransform);
        WeaponTrigger.SetActive(false);
    }

    public abstract bool ExecuteSkill(IHitable aim);
    public abstract void SetData(Creature creature, Action invokeAnimation, Action<bool> InvokeSkillHitStatus, Transform _transform);
}


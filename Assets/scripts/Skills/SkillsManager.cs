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
    public DamageOutput MainDamageOutput { get; set; }
    public DamageOutput SecondDamageOutput { get; set; }
    public Action InvokeAnimation { get;  set; }
    public Action<bool> InvokeSkillHitStatus { get; set; }
    public Transform mainPlayerTransform { get;  set; }
    public bool IsCooldownActive { get;  set; }
    public void SetCooldown(bool isCooldown) { IsCooldownActive = isCooldown;}
    public WeaponTriggerMelee WeaponTriggerMelee { get;  set; }

    public Skill() { }

    

    public abstract bool ExecuteSkill(IHitable aim);
    public abstract void SetData(Creature creature, WeaponTriggerMelee weaponTriggerMelee, Action invokeAnimation, Action<bool> InvokeSkillHitStatus, Transform _transform);
}


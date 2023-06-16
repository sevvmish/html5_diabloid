using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMeleeHit1h : Skill
{    
    private void Awake()
    {        
        SkillName = Localization.GetInstanse().GetCurrentTranslation().Skill01Name;
        SkillDescription = Localization.GetInstanse().GetCurrentTranslation().Skill01Description;
        Distance = 1.5f;
        Cooldown = 0.7f;
        DamageDistanceType = DamageDistanceTypes.melee;
    }

    public override void SetData(Creature creature, Action invokeAnimation)
    {
        PlayerData = creature;
        MainWeapon = PlayerData.MainInventory.MainWeapon;
        SecondWeapon = PlayerData.MainInventory.MainWeapon;

        InvokeAnimation = invokeAnimation;
        mainPlayerTransform = creature.PlayerTransform;
        WeaponTriggerMelee = creature.TriggerMelee;
        EffectsManager = creature.EffectsManager;
        MainDamageOutput = new DamageOutput(MainWeapon, PlayerData);
        SecondDamageOutput = new DamageOutput(SecondWeapon, PlayerData);
    }

    

    public override bool ExecuteSkill(IHitable aim)
    {
        bool isClose = (mainPlayerTransform.position - aim.AimTransform.position).magnitude <= (Distance + aim.PlayerRadius);

        if (MainWeapon == null)
        {
            return isClose;
        }

        if (aim == null)
        {
            if (!IsCooldownActive)
            {
                StartCoroutine(attack());
                return true;
            }
            
        }
        else
        {
            
            if (!IsCooldownActive && isClose)
            {
                StartCoroutine(attack());
            }

            return isClose;
        }

        return false;
    }
    private IEnumerator attack()
    {
        SetSkillCooldown(true);
        //InvokeSkillHitStatus?.Invoke(true);
        PlayerData.IsHitting = true;

        //InvokeAnimation?.Invoke();
        PlayerData.AnimationManager.HitAnimation();
        EffectsManager.PlaySound(SoundsType.swing1H_medium);
        
        WeaponTriggerMelee.UpdateConditions(Distance, 1, MainDamageOutput);        
        WeaponTriggerMelee.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(Time.fixedDeltaTime);
        
        WeaponTriggerMelee.gameObject.SetActive(false);
                
        yield return new WaitForSeconds(0.4f);

        //InvokeSkillHitStatus?.Invoke(false);
        PlayerData.IsHitting = false;

        yield return new WaitForSeconds(Cooldown - 0.4f);
        
        SetSkillCooldown(false);
    }
}


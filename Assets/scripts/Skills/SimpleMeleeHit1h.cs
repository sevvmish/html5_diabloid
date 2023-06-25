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
        TimeForPerform = 0.7f;
        DamageDistanceType = DamageDistanceTypes.melee;
    }

    public override void SetData(Creature creature, Action invokeAnimation, float cooldownKoeff)
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

        Cooldown *= cooldownKoeff;
    }

    

    public override bool ExecuteSkill(IHitable aim)
    {
        if (MainWeapon == null)
        {
            return false;
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
            bool isClose = (mainPlayerTransform.position - aim.AimTransform.position).magnitude <= (Distance + aim.PlayerRadius);
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
        PlayerData.IsHitting = true;

        PlayerData.AnimationManager.Hit1HAnimation();
        StartCoroutine(playSoundAfterDelay(0.1f, SoundsType.swing1H_medium));

        yield return new WaitForSeconds(0.1f);

        WeaponTriggerMelee.UpdateConditions(Distance, 1, MainDamageOutput);        
        WeaponTriggerMelee.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.15f);
        
        WeaponTriggerMelee.gameObject.SetActive(false);
                
        yield return new WaitForSeconds(TimeForPerform - 0.25f);
        

        PlayerData.IsHitting = false;

        yield return new WaitForSeconds(Cooldown - TimeForPerform - 0.25f);
        
        SetSkillCooldown(false);
    }

    private IEnumerator playSoundAfterDelay(float delay, SoundsType sound)
    {
        yield return new WaitForSeconds(delay);
        EffectsManager.PlaySound(sound);
    }
}


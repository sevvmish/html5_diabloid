using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMeleeDualHit1h : Skill
{
    private void Awake()
    {
        SkillName = Localization.GetInstanse().GetCurrentTranslation().Skill02Name;
        SkillDescription = Localization.GetInstanse().GetCurrentTranslation().Skill02Description;
        Distance = 1.5f;
        Cooldown = 0.5f;
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
        WeaponTriggerMelee.UpdateConditions(Distance, 1, MainDamageOutput);

        SetSkillCooldown(true);
        InvokeSkillHitStatus?.Invoke(true);
        WeaponTriggerMelee.gameObject.SetActive(true);

        yield return new WaitForSeconds(Time.fixedDeltaTime);

        WeaponTriggerMelee.gameObject.SetActive(false);
        InvokeAnimation?.Invoke();

        yield return new WaitForSeconds(0.5f);

        InvokeSkillHitStatus?.Invoke(false);

        yield return new WaitForSeconds(Cooldown - 0.3f);

        SetSkillCooldown(false);
    }
}

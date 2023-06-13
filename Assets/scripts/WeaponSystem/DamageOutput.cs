using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOutput
{
    public DamageTypes DamageType { get; private set; }
    public float FinalDamageAmount
    {
        get
        {
            return UnityEngine.Random.Range(DamageAmountMin, DamageAmountMax);
        }
    }
    public float DamageAmountMin { get; private set; }
    public float DamageAmountMax { get; private set; }
    public Creature Player { get; private set; }

    public DamageOutput(DamageTypes damageType, float damageAmountMin, float damageAmountMax, Creature player)
    {
        DamageType = damageType;
        DamageAmountMin = damageAmountMin;
        DamageAmountMax = damageAmountMax;
        Player = player;
    }

    public DamageOutput(Weapon weapon, Creature player)
    {
        DamageType = weapon.DamageType;
        DamageAmountMin = weapon.DamageAmountMin;
        DamageAmountMax = weapon.DamageAmountMax;
        Player = player;
    }


}
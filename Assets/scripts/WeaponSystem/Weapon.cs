using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon
{
    public WeaponTypes WeaponTypes { get; private set; }
    public WeaponDamage WeaponDamage { get; private set; }
    public string Name { get; private set; }
    public GameObject WeaponSkin { get; private set; }

    public Weapon(WeaponTypes weaponTypes, float damageAmountMin, float damageAmountMax, string name, int gameObjectID)
    {
        WeaponTypes = weaponTypes;
        Name = name;
        WeaponSkin = null;//GameManager.Instance.GetAssetManager.GetWeaponPack(gameObjectID);
        WeaponDamage = new WeaponDamage();

        switch (weaponTypes)
        {
            case WeaponTypes.sword1h:
                WeaponDamage = new WeaponDamage(DamageDistanceTypes.melee, DamageTypes.melee, damageAmountMin, damageAmountMax, 1.5f);
                break;
        }
    }

    public void SetWeaponDamageAmount(float damageAmountMin, float damageAmountMax) 
        => WeaponDamage.SetWeaponDamageAmount(damageAmountMin, damageAmountMax);
    
    public Weapon() { }

    public static Weapon GetFastWeaponById(int id, float damageAmountMin, float damageAmountMax)
    {        
        Translation translation = Localization.GetInstanse().GetCurrentTranslation();
        switch(id)
        {
            case 0: //-
                return null;
            case 1: //short sword
                return new Weapon(WeaponTypes.sword1h, damageAmountMin, damageAmountMax, translation.ShortSword, 1);
        }

        throw new NotImplementedException();
    }
}


public class WeaponDamage
{
    public WeaponDamage() { }

    public WeaponDamage(DamageDistanceTypes damageDistanceType, DamageTypes damageType, 
        float damageAmountMin, float damageAmountMax, float damageDistance)
    {
        DamageDistanceType = damageDistanceType;
        DamageType = damageType;
        DamageAmountMin = damageAmountMin;
        DamageAmountMax = damageAmountMax;
        DamageDistance = damageDistance;
    }

    public void SetWeaponDamageAmount(float damageAmountMin, float damageAmountMax)
    {
        DamageAmountMin = damageAmountMin;
        DamageAmountMax = damageAmountMax;
    }

    public DamageDistanceTypes DamageDistanceType { get; private set; }
    public DamageTypes DamageType { get; private set; }
    public float DamageAmountMin { get; private set; }
    public float DamageAmountMax { get; private set; }
    public float DamageDistance { get; private set; }
    public float DamageAmount { get => UnityEngine.Random.Range(DamageAmountMin, DamageAmountMax); }
}

public enum DamageDistanceTypes
{
    melee,
    ranged
}

public enum DamageTypes
{
    melee,
    ranged_melee,
    magic
}


public enum WeaponTypes
{
    none,
    sword1h,
    sword2h,
    dagger,
    axe1h,
    axe2h
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon
{
    public WeaponTypes WeaponTypes { get; private set; }
    public DamageDistanceTypes DamageDistanceType { get; private set; }
    public DamageTypes DamageType { get; private set; }
    public float DamageAmountMin { get; private set; }
    public float DamageAmountMax { get; private set; }
    public string Name { get; private set; }
    public GameObject WeaponSkin { get; private set; }

    public Weapon(WeaponTypes weaponTypes, DamageDistanceTypes damageDistanceType, DamageTypes damageType, float damageAmountMin, float damageAmountMax, string name, int gameObjectID)
    {
        WeaponTypes = weaponTypes;
        DamageDistanceType = damageDistanceType;
        DamageType = damageType;
        Name = name;
        WeaponSkin = null;//GameManager.Instance.GetAssetManager.GetWeaponPack(gameObjectID);
        DamageAmountMin = damageAmountMin;
        DamageAmountMax = damageAmountMax;
    }

    public void SetWeaponDamageAmount(float damageAmountMin, float damageAmountMax)
    {
        DamageAmountMin = damageAmountMin;
        DamageAmountMax = damageAmountMax;
    }        
    
    public Weapon() { }

    public static Weapon GetFastWeaponById(int id, float damageAmountMin, float damageAmountMax)
    {        
        Translation translation = Localization.GetInstanse().GetCurrentTranslation();
        switch(id)
        {
            case 0: //-
                return null;
            case 1: //short sword
                return new Weapon(WeaponTypes.sword1h, DamageDistanceTypes.melee, DamageTypes.melee, 
                    damageAmountMin, damageAmountMax, translation.ShortSword, 1);
        }

        throw new NotImplementedException();
    }

    public static WeaponTriggerMelee CreateMeleeWeaponTrigger(Transform mainPlayerTransform, int ownerID, CreatureSides side)
    {
        GameObject WeaponTrigger = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        WeaponTrigger.transform.parent = mainPlayerTransform;
        WeaponTrigger.GetComponent<SphereCollider>().isTrigger = true;
        WeaponTrigger.GetComponent<MeshRenderer>().enabled = false;
        //Destroy(WeaponTrigger.GetComponent<MeshFilter>());
        WeaponTrigger.transform.localScale = Vector3.one;
        WeaponTrigger.transform.localPosition = new Vector3(0, 0.5f, 0);

        WeaponTriggerMelee result = WeaponTrigger.AddComponent<WeaponTriggerMelee>();
        WeaponTrigger.SetActive(false);
        result.SetBaseConditions(ownerID, mainPlayerTransform, CreatureSides.AllGood);
        return result;
    }
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

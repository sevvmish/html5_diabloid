using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Weapon MainWeapon;
    public Weapon SecondWeapon;

    public Inventory() { }
    public Inventory(int mainWeaponID, float minMainWeapDamage, float maxMainWeapDamage, 
        int secondWeaponID, float minSecondWeapDamage, float maxSecondWeapDamage) 
    {
        MainWeapon = Weapon.GetFastWeaponById(mainWeaponID, minMainWeapDamage, maxMainWeapDamage);
        SecondWeapon = Weapon.GetFastWeaponById(secondWeaponID, minSecondWeapDamage, maxSecondWeapDamage);
    }

    public Inventory(PlayerData player)
    {
        MainWeapon = Weapon.GetFastWeaponById(player.MainWeaponID, player.MainWeaponMinDamage, player.MainWeaponMaxDamage);
        SecondWeapon = Weapon.GetFastWeaponById(player.SecondWeaponID, player.SecondWeaponMinDamage, player.SecondWeaponMaxDamage);
    }

    public Inventory(int mainWeaponID, float minMainWeapDamage, float maxMainWeapDamage)
    {
        MainWeapon = Weapon.GetFastWeaponById(mainWeaponID, minMainWeapDamage, maxMainWeapDamage);
        SecondWeapon = null;
    }
}

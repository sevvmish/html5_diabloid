using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Level;
    public int PlayerClass;
    public string Name;

    public int MainWeaponID;
    public float MainWeaponMinDamage;
    public float MainWeaponMaxDamage;

    public int SecondWeaponID;
    public float SecondWeaponMinDamage;
    public float SecondWeaponMaxDamage;
}

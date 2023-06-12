using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName ="asset", menuName = "ScriptableObjects/my assets")]
public class AssetLink : ScriptableObject
{
    public ushort AssetId;
    public AssetTypes AssetType;
    public GameObject MainGameObject;
}

[CreateAssetMenu(fileName = "asset", menuName = "ScriptableObjects/weapons")]
public class WeaponAssetLink : AssetLink
{
    public Sprite Sprite;
    public Weapon Weapon;
}

public enum AssetTypes
{
    playerSkin,
    NPCSkin,
    beastSkin,
    weapon,
    armor
}

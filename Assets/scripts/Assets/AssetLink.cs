using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="asset", menuName = "ScriptableObjects/my assets")]
public class AssetLink : ScriptableObject
{
    public ushort AssetId;
    public AssetTypes AssetType;
    public GameObject gameObject;
}

public enum AssetTypes
{
    player,
    NPC,
    beast
}

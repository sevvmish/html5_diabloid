using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssetManager : MonoBehaviour
{
    public AssetLink[] PlayerSkinPack;
    public AssetLink[] NPCSkinPack;
    public AssetLink[] BeastSkinPack;
    public AssetLink[] WeaponPack;
    public AssetLink[] ArmorPack;


    public GameObject GetPlayerSkinPack(int id)
    {
        if (PlayerSkinPack.Length == 0) return null;

        for (int i = 0; i < PlayerSkinPack.Length; i++)
        {
            if (PlayerSkinPack[i].AssetId == id) return PlayerSkinPack[i].MainGameObject;
        }

        return null;
    }

    public GameObject GetNPCSkinPack(int id)
    {
        if (NPCSkinPack.Length == 0) return null;

        for (int i = 0; i < NPCSkinPack.Length; i++)
        {
            if (NPCSkinPack[i].AssetId == id) return NPCSkinPack[i].MainGameObject;
        }

        return null;
    }

    public GameObject GetBeastSkinPack(int id)
    {
        if (BeastSkinPack.Length == 0) return null;

        for (int i = 0; i < BeastSkinPack.Length; i++)
        {
            if (BeastSkinPack[i].AssetId == id) return BeastSkinPack[i].MainGameObject;
        }

        return null;
    }

    public GameObject GetWeaponPack(int id)
    {
        if (WeaponPack.Length == 0) return null;

        for (int i = 0; i < WeaponPack.Length; i++)
        {
            if (WeaponPack[i].AssetId == id) return WeaponPack[i].MainGameObject;
        }

        return null;
    }

    public GameObject GetArmorPack(int id)
    {
        if (ArmorPack.Length == 0) return null;

        for (int i = 0; i < ArmorPack.Length; i++)
        {
            if (ArmorPack[i].AssetId == id) return ArmorPack[i].MainGameObject;
        }

        return null;
    }
}

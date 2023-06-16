using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;


public class AssetManager : MonoBehaviour
{
    
    public static GameObject GetPlayerSkinByID(int id)
    {
        switch(id)
        {
            case 0:
                return null;

            case 1:
                return Resources.Load<GameObject>("heroes/Character_Knight_01_Black");
            case 2:
                return Resources.Load<GameObject>("enemies/SkeletonWarrior");

        }

        return null;
    }

    public static GameObject GetWeaponByID(int id)
    {
        switch (id)
        {
            case 0:
                return null;

            case 1:
                return Resources.Load<GameObject>("weapons/short sword");
            case 2:
                return Resources.Load<GameObject>("weapons/axe 1h 01");

        }

        return null;
    }

}

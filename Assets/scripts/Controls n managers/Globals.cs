using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Globals: MonoBehaviour 
{
    public static Vector3 defaultCameraPositionMobile = new Vector3(0, 6.7f, -4f);//0    7.2    -4.3
    public static Vector3 defaultCameraRotationMobile = new Vector3(60, 0, 0);//60

    public static Vector3 defaultCameraPositionPC = new Vector3(0, 8f, -4.7f);//0    7.2    -4.3
    public static Vector3 defaultCameraRotationPC = new Vector3(60, 0, 0);//60

    public static bool IsPlatformMobile;

    public static PlayerData MainPlayerData;
    public static Creature MainPlayerEntity;

    public static int MessageFontSize { get
        {
            if (IsPlatformMobile)
            {
                return 50;
            }
            else
            {
                return 40;
            }
        } }

    public static void GetPlayerEntity()
    {
        SaveLoad.LoadMainPlayer();
        int level = MainPlayerData.Level;
        int playerClass = MainPlayerData.PlayerClass;

        switch ((MainPlayerClasses)playerClass)
        {
            case MainPlayerClasses.Barbarian:
                MainPlayerEntity = new Barbarian(level, 1);
                break;

            case MainPlayerClasses.Mage:
                MainPlayerEntity = new Barbarian(level, 2);
                break;

            case MainPlayerClasses.Rogue:
                MainPlayerEntity=  new Barbarian(level, 3);
                break;
        }

        MainPlayerEntity.SetInventory(new Inventory(MainPlayerData));
    }
}

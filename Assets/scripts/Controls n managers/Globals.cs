using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Globals: MonoBehaviour 
{
    public static bool IsJoystick;
    public static PlayerData MainPlayerData;
    public static Creature MainPlayerEntity;

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

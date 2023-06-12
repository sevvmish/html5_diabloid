using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static bool IsJoystick;
    public static PlayerData MainPlayerData;

    public static Creature GetPlayerEntity()
    {
        SaveLoad.LoadMainPlayer();
        int level = MainPlayerData.Level;
        int playerClass = MainPlayerData.PlayerClass;

        switch ((MainPlayerClasses)playerClass)
        {
            case MainPlayerClasses.Barbarian:
                return new Barbarian(level, 1);

            case MainPlayerClasses.Mage:
                return new Barbarian(level, 2);

            case MainPlayerClasses.Rogue:
                return new Barbarian(level, 3);
        }

        throw new NotImplementedException();
    }
}

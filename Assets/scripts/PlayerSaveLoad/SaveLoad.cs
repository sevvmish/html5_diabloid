using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad
{
    public static void SaveMainPlayer()
    {
        saveToPrefs(Globals.MainPlayerData);
    }

    public static void LoadMainPlayer()
    {
        Globals.MainPlayerData = loadFromPrefs();
        /*
        PlayerData player = new PlayerData();
        player.Level = 1;
        player.PlayerClass = 1;
        player.PlayerName = "Player";

        player.MainWeaponID = 1;
        player.MainWeaponMinDamage = 3;
        player.MainWeaponMaxDamage = 5;

        player.SecondWeaponID = 0;
        player.SecondWeaponMinDamage = 0;
        player.SecondWeaponMaxDamage = 0;
        Globals.MainPlayerData = player;*/
    }

    private static PlayerData loadFromPrefs()
    {
        PlayerData player = new PlayerData();
        int saverID = 18;

        //main data
        if (PlayerPrefs.GetInt("SaverId") == saverID)
        {
            Debug.Log("save file is OK!");
            player.Level = PlayerPrefs.GetInt("Level");
            player.PlayerClass = PlayerPrefs.GetInt("PlayerClass");
            player.PlayerName = PlayerPrefs.GetString("PlayerName");
            
            player.MainWeaponID = PlayerPrefs.GetInt("MainWeaponID");
            player.MainWeaponMinDamage = PlayerPrefs.GetFloat("MainWeaponMinDamage");
            player.MainWeaponMaxDamage = PlayerPrefs.GetFloat("MainWeaponMaxDamage");

            player.SecondWeaponID = PlayerPrefs.GetInt("SecondWeaponID");
            player.SecondWeaponMinDamage = PlayerPrefs.GetFloat("SecondWeaponMinDamage");
            player.SecondWeaponMaxDamage = PlayerPrefs.GetFloat("SecondWeaponMaxDamage");

        }
        else
        {
            Debug.Log("save file is NOT OK! Dafaults loaded!");
            PlayerPrefs.SetInt("SaverId", saverID);
            player.Level = 1;
            player.PlayerClass = 1;
            player.PlayerName = "Player";
            
            player.MainWeaponID = 1;
            player.MainWeaponMinDamage = 3;
            player.MainWeaponMaxDamage = 5;

            player.SecondWeaponID = 2;
            player.SecondWeaponMinDamage = 3;
            player.SecondWeaponMaxDamage = 5;

            saveToPrefs(player);

        }

        return player;        
    }

    private static void saveToPrefs(PlayerData player)
    {
        
        PlayerPrefs.SetInt("Level", player.Level);
        PlayerPrefs.SetInt("PlayerClass", player.PlayerClass);
        PlayerPrefs.SetString("PlayerName", player.PlayerName);
        
        PlayerPrefs.SetInt("MainWeaponID", player.MainWeaponID);
        PlayerPrefs.SetFloat("MainWeaponMinDamage", player.MainWeaponMinDamage);
        PlayerPrefs.SetFloat("MainWeaponMaxDamage", player.MainWeaponMaxDamage);

        PlayerPrefs.SetInt("SecondWeaponID", player.SecondWeaponID);
        PlayerPrefs.SetFloat("SecondWeaponMinDamage", player.SecondWeaponMinDamage);
        PlayerPrefs.SetFloat("SecondWeaponMaxDamage", player.SecondWeaponMaxDamage);
    }

    
}

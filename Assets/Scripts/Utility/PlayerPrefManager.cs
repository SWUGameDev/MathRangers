using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerPrefManager
{

    public static readonly string PlayerLevelKey = "Key_PlayerLevel";
    public static readonly string GameMoneyKey = "Key_GameMoney";

    public static readonly string PlayerItemWearKey = "Key_PlayerItemWear";

    public static readonly string PlayerItemOwnedKey = "Key_PlayerItemOwned";

    public static void SetInt(string key,int data)
    {
        PlayerPrefs.SetInt(key,data);
    }

    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void SetString(string key,string data)
    {
        PlayerPrefs.SetString(key,data);
    }

    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerPrefManager
{

    public static readonly string GameMoneyKey = "Key_GameMoney";

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
}
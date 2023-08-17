using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerPrefManager
{

    public static void SetInt(string key,int data)
    {
        PlayerPrefs.SetInt(key,data);
    }
}
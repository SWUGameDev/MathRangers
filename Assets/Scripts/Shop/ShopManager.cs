using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private int playerMoney;

    void Start()
    {
        this.LoadPlayerMoney();
    }

    private void LoadPlayerMoney()
    {
        this.playerMoney = PlayerPrefs.GetInt("");
    }

}

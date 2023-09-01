using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheatController : MonoBehaviour
{
    [SerializeField] MoneyUIController moneyUIController;

    [SerializeField] TMP_Text levelText;
    int moneyCount = 0;

    int levelCount = 0;

    int maxCount = 15;

    bool isMoneyCheatActive = false; 

    bool isLevelCheatActive = false; 
    public void onProfileClicked()
    {
        this.moneyCount++;

        if(this.moneyCount>this.maxCount && this.isMoneyCheatActive == false)
        {
            PlayerPrefManager.SetInt(PlayerPrefManager.GameMoneyKey,100000);
            moneyUIController.SetMoneyData(100000);
            this.moneyCount = 0;
            this.isMoneyCheatActive = true;
        }else if(this.moneyCount>this.maxCount && this.isMoneyCheatActive)
        {
            PlayerPrefManager.SetInt(PlayerPrefManager.GameMoneyKey,0);
            moneyUIController.SetMoneyData(0);
            this.moneyCount = 0;
            this.isMoneyCheatActive = false;
        }
    }

    public void onLevelClicked()
    {
        this.levelCount++;

        if(this.levelCount>this.maxCount && this.isLevelCheatActive == false)
        {
            PlayerPrefs.SetInt(PlayerPrefManager.PlayerLevelKey,5);
            this.levelCount = 0;
            this.levelText.text = "Lv 5";
        }
    }
}

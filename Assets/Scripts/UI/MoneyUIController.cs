using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    void Start()
    {
        this.moneyText.text = PlayerPrefManager.GetInt(PlayerPrefManager.GameMoneyKey).ToString();
    }

    
    public void SetMoneyData(int data)
    {
        this.moneyText.text = data.ToString();
    }

}

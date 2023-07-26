using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RunSceneUIManager : UI_Base
{
    [SerializeField] GameObject playerGameObject;
    private RunPlayer runPlayer;
    private int eatCheeseNumber = 0;
    [SerializeField] TMP_Text eatCheeseNumberText;
    [SerializeField] Slider playerHpSlider;

    private void Awake()
    {
        runPlayer = playerGameObject.GetComponent<RunPlayer>();
        runPlayer.onEatCheese.AddListener(this.EatCheeseNumber);
        runPlayer.onCollisionEnemy.AddListener(this.SetHpGauge);
    }

    private void Start()
    {
        eatCheeseNumberText.text = eatCheeseNumber.ToString();
        playerHpSlider.value = 1;
    }

    private void OnDestroy()
    {
        runPlayer.onEatCheese.RemoveListener(this.EatCheeseNumber);
        runPlayer.onCollisionEnemy.RemoveListener(this.SetHpGauge);
    }

    public void EatCheeseNumber()
    {
        Debug.Log("eatCheeseNumber");
        eatCheeseNumber++;
        eatCheeseNumberText.text = eatCheeseNumber.ToString();
    }

    private void SetHpGauge()
    {
        this.playerHpSlider.value = runPlayer.PlayerHp / runPlayer.MaxPlayerHp;
    }
}

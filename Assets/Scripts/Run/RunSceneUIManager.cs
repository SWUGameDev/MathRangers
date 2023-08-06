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
    [SerializeField] GameObject mathPanel;

    [SerializeField] public RunUIBackGroundScrolling windowScrolling;
    [SerializeField] RunUIBackGroundScrolling cloudScrolling;
    [SerializeField] RunUIBackGroundScrolling tileScrolling;
    [SerializeField] RunUIBackGroundScrolling cheezeScrolling;

    [SerializeField] private CountdownController countdownController;
    private void Awake()
    {
        runPlayer = playerGameObject.GetComponent<RunPlayer>();
        runPlayer.onEatCheese.AddListener(this.EatCheeseNumber);
        runPlayer.onCollisionEnemy.AddListener(this.SetHpGauge);
        runPlayer.onTriggerMath.AddListener(this.SetAllScroll);

    }

    private void Start()
    {
        eatCheeseNumberText.text = eatCheeseNumber.ToString();
        playerHpSlider.value = 1;

        this.countdownController.StartCountdown(this.SetAllScroll);
    }

    private void OnDestroy()
    {
        runPlayer.onEatCheese.RemoveListener(this.EatCheeseNumber);
        runPlayer.onCollisionEnemy.RemoveListener(this.SetHpGauge);
        runPlayer.onTriggerMath.RemoveAllListeners();
    }

    public void EatCheeseNumber()
    {
        Debug.Log("eatCheeseNumber");
        eatCheeseNumber++;
        eatCheeseNumberText.text = eatCheeseNumber.ToString();
    }


    public void SetAllScroll()
    {
        windowScrolling.SetisScroll();
        cloudScrolling.SetisScroll();
        tileScrolling.SetisScroll();
        cheezeScrolling.SetisScroll();
    }

    private void SetHpGauge()
    {
        this.playerHpSlider.value = runPlayer.PlayerHp / runPlayer.MaxPlayerHp;
    }

}

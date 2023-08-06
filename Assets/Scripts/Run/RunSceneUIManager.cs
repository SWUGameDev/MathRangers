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
    [SerializeField] GameObject deadPanel;

    [SerializeField] TMP_Text AnswerRateText;
    private int latestAnswerRate;
    [SerializeField] MathQuestionExtension mathQuestionExtension;

    private void Awake()
    {
        runPlayer = playerGameObject.GetComponent<RunPlayer>();
        runPlayer.onEatCheese.AddListener(this.EatCheeseNumber);
        runPlayer.onCollisionEnemy.AddListener(this.SetHpGauge);
        runPlayer.onTriggerMath.AddListener(this.SetAllScroll);
        runPlayer.onRunPlayerDead.AddListener(this.SetDeadPanel);
        MathQuestionExtension.OnQuestionSolved += GetAnswerRate;
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
        runPlayer.onRunPlayerDead.RemoveAllListeners();
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

    private void SetDeadPanel()
    {
        this.StartCoroutine(this.SetDeadPanelCoroutine());
    }

    public IEnumerator SetDeadPanelCoroutine()
    {
        SetAllScroll();
        yield return new WaitForSeconds(0.8f);
        deadPanel.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        deadPanel.SetActive(false);
        SetAllScroll();
    }

    void GetAnswerRate(int index, bool isCorrect)
    {
        Debug.Log("GetAnswerRate");
        Debug.Log(index);
        Debug.Log(isCorrect);


        int sum = latestAnswerRate * (index - 1);
        if (isCorrect == true)
        {
            latestAnswerRate = (sum + 100) / index;
        }
        else if (isCorrect == false) 
        {
            latestAnswerRate = (sum + 0) / index;
        }

        SetAnswerRate();
    }

    void SetAnswerRate()
    {
        AnswerRateText.text = latestAnswerRate.ToString() + "%"; 
    }
}

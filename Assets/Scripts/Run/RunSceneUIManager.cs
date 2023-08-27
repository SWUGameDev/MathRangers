using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Newtonsoft.Json;
using WjChallenge;

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
    [SerializeField] RunUIBackGroundScrolling endScrolling;
    [SerializeField] RunUIBackGroundScrolling testScrolling;

    [SerializeField] private CountdownController countdownController;
    [SerializeField] GameObject deadPanel;

    [SerializeField] TMP_Text AnswerRateText;
    private float latestAnswerRate;
    [SerializeField] MathQuestionExtension mathQuestionExtension;

    [SerializeField] StopPanelController stopPanelController;
    [SerializeField] GameResultUIController gameResultUIController;

    [SerializeField] private Image[] selectAbilityOnPlayImages;
    [SerializeField] private Image[] selectAbilityOnStopImages;
    private int abilityIndex = 0;
    private float minY;
    public float MinY
    {
        get { return minY; }
    }

    private int solveIndex;
    private float sum;
    private void Awake()
    {
        runPlayer = playerGameObject.GetComponent<RunPlayer>();
        runPlayer.onEatCheese.AddListener(this.EatCheeseNumber);
        runPlayer.onSetHpGauge.AddListener(this.SetHpGauge);
        runPlayer.onTriggerMath.AddListener(this.SetAllScroll);
        runPlayer.onRunPlayerDead.AddListener(this.SetDeadPanel);
        MathQuestionExtension.OnQuestionSolved += GetAnswerRate;
        this.countdownController.StartCountdown(this.GameStartUISetting);
        SoundManager.Instance.ChangeBackgroundAudioSource(backgroundAudioSourceType.BGM_RUN);
    }

    private void Start()
    {
        minY = CalculateScreenMinY();
        sum = 0;
        eatCheeseNumberText.text = eatCheeseNumber.ToString();
        playerHpSlider.value = runPlayer.PlayerHp / runPlayer.MaxPlayerHp;
    }

    private void OnDestroy()
    {
        runPlayer.onEatCheese.RemoveListener(this.EatCheeseNumber);
        runPlayer.onSetHpGauge.RemoveListener(this.SetHpGauge);
        runPlayer.onTriggerMath.RemoveAllListeners();
        runPlayer.onRunPlayerDead.RemoveAllListeners();
    }

    public void EatCheeseNumber()
    {
        eatCheeseNumber++;
        RunBossItemManager.Instance.CheeseFromRunGame = eatCheeseNumber;
        eatCheeseNumberText.text = eatCheeseNumber.ToString();
    }

    public void GameStartUISetting()
    {
        SetAllScroll(true);
        runPlayer.isRun = true;
    }

    public void SetAllScroll(bool isEnabled)
    {
        windowScrolling.SetisScroll(isEnabled);
        cloudScrolling.SetisScroll(isEnabled);
        tileScrolling.SetisScroll(isEnabled);
        cheezeScrolling.SetisScroll(isEnabled);
        endScrolling.SetisScroll(isEnabled);
        testScrolling.SetisScroll(isEnabled);
        runPlayer.isRun = isEnabled;
    }

    public void SetAllReverse(bool isEnabled)
    {
        windowScrolling.SetisReverse(isEnabled);
        cloudScrolling.SetisReverse(isEnabled);
        tileScrolling.SetisReverse(isEnabled);
        cheezeScrolling.SetisReverse(isEnabled);
        endScrolling.SetisReverse(isEnabled);
        testScrolling.SetisReverse(isEnabled);
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
        SetAllScroll(false);
        yield return new WaitForSeconds(0.8f);
        deadPanel.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        deadPanel.SetActive(false);
        SetAllScroll(true);
    }

    void GetAnswerRate(int index, bool isCorrect)
    {
        if (isCorrect == true)
        {
            sum += 100;
        }

        latestAnswerRate = (int)(sum / index);
        SetAnswerRate();
        stopPanelController.SetQuestionCorrectRate((int)latestAnswerRate);

        // 분리하기
        // ResultTest
        Debug.Log(index);
        solveIndex = index;
    }

    public void GameResultSuccess()
    {
        if (solveIndex == 8)
        {
            if (!PlayerPrefs.HasKey(GameResultUIController.responseLearningProgressDataKey))
                return;

            string data = PlayerPrefs.GetString(GameResultUIController.responseLearningProgressDataKey);
            Response_Learning_ProgressData response_Learning_ProgressData = JsonConvert.DeserializeObject<Response_Learning_ProgressData>(data);

            this.gameResultUIController.SetResult(GameResultType.MissionSuccess, new GameResultData(0, 0, eatCheeseNumber), response_Learning_ProgressData);
        }
    }

    void SetAnswerRate()
    {
        AnswerRateText.text = latestAnswerRate.ToString() + "%"; 
    }

    public float CalculateScreenMinY()
    {
        Vector3 screenMin = Camera.main.ScreenToWorldPoint(Vector3.zero);
        return screenMin.y;
    }

    public void ShowAbilityOnScreen(AbilityInfo abilityInfo)
    {
        selectAbilityOnPlayImages[abilityIndex].sprite = abilityInfo.abilityIcon;
        selectAbilityOnStopImages[abilityIndex].sprite = abilityInfo.abilityIcon;
        abilityIndex++;
    }
}

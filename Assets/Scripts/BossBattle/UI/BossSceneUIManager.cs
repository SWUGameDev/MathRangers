using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using WjChallenge;

public partial class BossSceneUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text limitTimeText;

    [SerializeField] private float limitTimeSeconds;

    public int deadMinionNumber;
    [SerializeField] private TextMeshProUGUI deadMinionNumberText;
    [SerializeField] private Slider bossHpslider;
    [SerializeField] private TMP_Text bossHpText;
    [SerializeField] private GameObject bossGameObject;
    private Boss boss;
    [SerializeField] GameResultUIController gameResultUIController;
    //TODO : 시간 나면 로직 분리하기

    private void Start()
    {
        boss = bossGameObject.GetComponent<Boss>();
        bossHpslider.value = 1;
        deadMinionNumber = 0;
        boss.BossHp = boss.MaxBossHp;
        bossHpText.text = boss.BossHp.ToString();

        Player.OnBossDamaged.AddListener(this.SetBossHpGauge);
        Minion.OnMinionDead.AddListener(this.SetMinionNumber);
    }

    private void Update() {

        this.limitTimeSeconds -= Time.deltaTime;

        if(this.limitTimeSeconds <= 0)
        {
            //TODO : GameEnd 호출하기
            GameResultMission();
        }

        TimeSpan time = TimeSpan.FromSeconds(this.limitTimeSeconds);
        this.limitTimeText.text = time.ToString(@"mm\:ss");
    }

    private void OnDestroy()
    {
        Player.OnBossDamaged.RemoveListener(this.SetBossHpGauge);
        Minion.OnMinionDead.RemoveListener(this.SetMinionNumber);
    }

    public void SetMinionNumber()
    {
        deadMinionNumber++;
        deadMinionNumberText.text = deadMinionNumber.ToString();
    }

    private void SetBossHpGauge(int damage)
    {
        boss.BossHp -= damage;
        bossHpText.text = boss.BossHp.ToString();
        this.bossHpslider.value = boss.BossHp / boss.MaxBossHp;
    }

    public void GameResultMissionFail()
    {
        if (!PlayerPrefs.HasKey(GameResultUIController.responseLearningProgressDataKey))
            return;

        string data = PlayerPrefs.GetString(GameResultUIController.responseLearningProgressDataKey);
        Response_Learning_ProgressData response_Learning_ProgressData = JsonConvert.DeserializeObject<Response_Learning_ProgressData>(data);

        this.gameResultUIController.SetResult(GameResultType.MissionFail, new GameResultData(0, 0, 0), response_Learning_ProgressData);
    }

    public void GameResultMission()
    {
        if (!PlayerPrefs.HasKey(GameResultUIController.responseLearningProgressDataKey))
            return;

        string data = PlayerPrefs.GetString(GameResultUIController.responseLearningProgressDataKey);
        Response_Learning_ProgressData response_Learning_ProgressData = JsonConvert.DeserializeObject<Response_Learning_ProgressData>(data);

        this.gameResultUIController.SetResult(GameResultType.MissionSuccess, new GameResultData(0, 0, 0), response_Learning_ProgressData);
    }

}

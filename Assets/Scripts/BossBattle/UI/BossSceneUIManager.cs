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

    private float limitTimeSeconds;

    public int deadMinionNumber;
    [SerializeField] private TextMeshProUGUI deadMinionNumberText;
    [SerializeField] private Slider bossHpslider;
    [SerializeField] private TMP_Text bossHpText;
    [SerializeField] private GameObject bossGameObject;
    private Boss boss;
    [SerializeField] GameResultUIController gameResultUIController;
    //TODO : 시간 나면 로직 분리하기
    long score = 0;
    bool isEnd = false;
    [SerializeField] private Player player;

    private void Start()
    {
        limitTimeSeconds = player.playerProperty.LimitTime;
        boss = bossGameObject.GetComponent<Boss>();
        bossHpslider.value = 1;
        deadMinionNumber = 0;
        boss.BossHp = boss.MaxBossHp;
        bossHpText.text = score.ToString();

        Player.OnBossDamaged.AddListener(this.SetBossHpGauge);
        Minion.OnMinionDead.AddListener(this.SetMinionNumber);
    }

    private void Update() {

        this.limitTimeSeconds -= Time.deltaTime;

        if(this.limitTimeSeconds <= 0 && this.isEnd == false)
        {
            this.isEnd = true;
            GameResultMissionSuccess();
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
        score += (long)damage;
        bossHpText.text = score.ToString();
        this.bossHpslider.value = boss.BossHp / boss.MaxBossHp;
    }

    public void GameResultMissionFail()
    {
        SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_FAIL);

        if (!PlayerPrefs.HasKey(GameResultUIController.responseLearningProgressDataKey))
            return;

        string data = PlayerPrefs.GetString(GameResultUIController.responseLearningProgressDataKey);
        Response_Learning_ProgressData response_Learning_ProgressData = JsonConvert.DeserializeObject<Response_Learning_ProgressData>(data);

        if (!PlayerPrefs.HasKey("eatCheeseNumber"))
            return;

        int eatCheeseNumber = PlayerPrefs.GetInt("eatCheeseNumber");

        this.gameResultUIController.SetResult(GameResultType.MissionFail, new GameResultData(this.score, this.deadMinionNumber, eatCheeseNumber), response_Learning_ProgressData);
    }

    public void GameResultMissionSuccess()
    {
        SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_SUCCESS);

        if (!PlayerPrefs.HasKey(GameResultUIController.responseLearningProgressDataKey))
            return;

        string data = PlayerPrefs.GetString(GameResultUIController.responseLearningProgressDataKey);
        Response_Learning_ProgressData response_Learning_ProgressData = JsonConvert.DeserializeObject<Response_Learning_ProgressData>(data);

        if (!PlayerPrefs.HasKey("eatCheeseNumber"))
            return;

        int eatCheeseNumber = PlayerPrefs.GetInt("eatCheeseNumber");

        this.gameResultUIController.SetResult(GameResultType.MissionSuccess, new GameResultData(this.score, this.deadMinionNumber, eatCheeseNumber), response_Learning_ProgressData);
    }
}

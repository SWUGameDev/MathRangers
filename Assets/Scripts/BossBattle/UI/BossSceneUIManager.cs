using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using static Minion;

public partial class BossSceneUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text limitTimeText;

    [SerializeField] private float limitTimeSeconds;

    public int deadMinionNumber;
    [SerializeField] private TextMeshProUGUI deadMinionNumberText;
    [SerializeField] private Slider bossHpslider;
    [SerializeField] private TMP_Text bossHpText;

    private float maxBossHp = 20000;
    private float bossHp; // 보스로 이동

    //TODO : 시간 나면 로직 분리하기

    private void Start()
    {
        bossHpslider.value = 1;
        deadMinionNumber = 0;
        bossHp = maxBossHp;
        bossHpText.text = bossHp.ToString();

        Player.OnBossDamaged.AddListener(this.SetBossHpGauge);

        Minion.OnMinionDead.AddListener(this.SetMinionNumber);
    }

    private void Update() {

        this.limitTimeSeconds -= Time.deltaTime;

        if(this.limitTimeSeconds>=0)
        {
            //TODO : GameEnd 호출하기
        }

        TimeSpan time = TimeSpan.FromSeconds(this.limitTimeSeconds);
        this.limitTimeText.text = time.ToString(@"mm\:ss");
    }



    public void SetMinionNumber()
    {
        deadMinionNumber++;
        deadMinionNumberText.text = deadMinionNumber.ToString();
    }

    private void SetBossHpGauge(int damage)
    {
        bossHp -= damage;
        bossHpText.text = bossHp.ToString();
        this.bossHpslider.value = bossHp/maxBossHp;
    }
}

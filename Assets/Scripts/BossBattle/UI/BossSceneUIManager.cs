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
    private float maxBossHp = 20000;
    private float bossHp;

    //TODO : 시간 나면 로직 분리하기

    private void Start()
    {
        bossHpslider.value = 1;
        deadMinionNumber = 0;
        bossHp = maxBossHp;
        Player.OnBossDamaged.AddListener(this.setBossHpGauge);

        Minion.OnMinionAttacked.AddListener(this.setMinionNumber);
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



    public void setMinionNumber()
    {
        deadMinionNumber++;
        deadMinionNumberText.text = deadMinionNumber.ToString();
    }

    private void setBossHpGauge(int damage)
    {
        bossHp -= damage;
        this.bossHpslider.value = bossHp/maxBossHp;
    }
}

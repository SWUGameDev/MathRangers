using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        if(this.limitTimeSeconds>=0)
        {
            //TODO : GameEnd 호출하기
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
}

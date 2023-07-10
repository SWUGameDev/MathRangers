using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public partial class BossSceneUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text limitTimeText;

    [SerializeField] private float limitTimeSeconds;

    //TODO : 시간 나면 로직 분리하기
    private void Update() {

        this.limitTimeSeconds -= Time.deltaTime;

        if(this.limitTimeSeconds>=0)
        {
            //TODO : GameEnd 호출하기
        }

        TimeSpan time = TimeSpan.FromSeconds(this.limitTimeSeconds);
        this.limitTimeText.text = time.ToString(@"mm\:ss");
    }

    private void Start() {
        
    }
}

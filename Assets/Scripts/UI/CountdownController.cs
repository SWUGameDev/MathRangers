using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CountdownController : MonoBehaviour
{
    [SerializeField]
    private float startTime = 3;

    private float remainTime;

    [SerializeField]
    private TMP_Text countdownText;

    private CustomYieldInstruction waitForSecondsRealtime;

    private void Awake() {
        this.waitForSecondsRealtime = new WaitForSecondsRealtime(1);

        this.StartCountdown();
    }

    /**
    SetStartTime

    카운트 다운 시간 초기값 지정합니다.
    기본값은 3초입니다.
    */
    public void SetStartTime(float time)
    {
        this.startTime = time;
    }

    /**
    StartCountdown

    카운트 다운을 진행합니다.
    옵셔널 파라미터 OnCountdownCompleted로 카운트다운 이후 액션을 전달할 수 있습니다.
    */
    public void StartCountdown(Action OnCountdownCompleted = null)
    {
        this.gameObject.SetActive(true);

        this.StartCoroutine(this.StartCountdownCoroutine(OnCountdownCompleted));
    }
    private IEnumerator StartCountdownCoroutine(Action OnCountdownCompleted = null)
    {
        this.ResetCountdown();

        yield return this.waitForSecondsRealtime;
        
        while(this.remainTime>0)
        {  
            this.countdownText.text = this.remainTime.ToString();
            yield return this.waitForSecondsRealtime;
            this.remainTime--;           
        }

        this.gameObject.SetActive(false);
        OnCountdownCompleted?.Invoke();
    }

    private void ResetCountdown()
    {
        this.remainTime = this.startTime;
    }
}

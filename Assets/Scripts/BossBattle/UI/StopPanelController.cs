using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StopPanelController : MonoBehaviour
{
    [SerializeField] Image[] buffImageArr = new Image[7];
    [SerializeField] Image[] questionImageArr = new Image[7];
    [SerializeField] TMP_Text correctRate;

    Color green = new Color(0f, 1f, 0f);
    Color red = new Color(1f, 0f, 0f);
    void Start()
    {
        for(int i = 0; i < 8; i++)
        {
            SetQuestionCorrect(i, true);
        }

        SetQuestionCorrectRate(100);
    }

    public void SetQuestionCorrect(int idx, bool isCorrect)
    {
        if(isCorrect == true)
        {
            questionImageArr[idx].color = green;
        }
        else
        {
            questionImageArr[idx].color = red;
        }
    }

    public void SetQuestionCorrectRate(int rate)
    {
        // float 로 받아서 소수점 없애기
        this.correctRate.text = rate.ToString() + '%';
    }
}

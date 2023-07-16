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
            SetQuestionCorrect(questionImageArr, i, true);
        }

        SetQuestionCorrectRate(100);
    }

    public void SetQuestionCorrect(Image[] arr, int idx, bool isCorrect)
    {
        if(isCorrect == true)
        {
            arr[idx].color = green;
        }
        else
        {
            arr[idx].color = red;
        }
    }

    public void SetQuestionCorrectRate(int rate)
    {
        this.correctRate.text = rate.ToString() + '%';
    }
}

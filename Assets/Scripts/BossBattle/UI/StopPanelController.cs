using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StopPanelController : MonoBehaviour
{
    [SerializeField] Image[] buffImageArr;
    [SerializeField] Image[] questionImageArr;
    [SerializeField] Image[] questionRunArr;
    [SerializeField] TMP_Text correctRate;
    [SerializeField] GameObject stopPanel;
    Color green = new Color(0f, 1f, 0f);
    Color red = new Color(1f, 0f, 0f);

    private void Awake()
    {
        MathQuestionExtension.OnQuestionSolved += SetQuestionCorrect;
    }
    void Start()
    {
        SetQuestionCorrectRate(100);
    }

    public void StopPanelSetActive()
    {
        stopPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StopPanelSetActiveFalse()
    {
        stopPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetQuestionCorrect(int mathindex, bool isCorrect)
    {
        int idx = mathindex - 1;
        Debug.Log(idx);
        if(isCorrect == true)
        {
            questionImageArr[idx].color = green;
            questionRunArr[idx].color = green;
        }
        else
        {
            questionImageArr[idx].color = red;
            questionRunArr[idx].color = red;
        }
    }

    public void SetQuestionCorrectRate(int rate)
    {
        this.correctRate.text = rate.ToString() + '%';
    }
}

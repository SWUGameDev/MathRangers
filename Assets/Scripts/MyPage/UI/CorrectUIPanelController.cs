using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CorrectUIPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text correctRateText;

    void Start()
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetCorrectPanelData;
        UserGameResultInfoManager.OnUserGameResultInfoInitialized += SetCorrectPanelData;  
    }
    private void SetCorrectPanelData(List<GameResultInfo> gameResultInfos)
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetCorrectPanelData;

        int correctRateSum = 0;
        foreach(GameResultInfo resultInfo in gameResultInfos)
        {
            correctRateSum += resultInfo.progressData.explAcrcyRt;
        }
        correctRateSum /= gameResultInfos.Count;
        this.SetCorrectRateText(correctRateSum.ToString());
    }

    private void SetCorrectRateText(string rate)
    {
        this.correctRateText.text = $"{rate}%";
    }
}

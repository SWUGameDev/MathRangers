using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public partial class TotalRecordController : MonoBehaviour
{
    [SerializeField] private UserGameResultInfoManager userGameResultInfoManager;

    private List<GameResultInfo> gameResultInfos;

    [SerializeField] private TMP_Text totalCountText;

    [SerializeField] private TMP_Text totalRateText;

    private List<BarData> correctBarDataList;

    [SerializeField] private GameObject barPrefab;

    [SerializeField] private Transform correctBarContentTransform;
    void Start()
    {
        this.gameResultInfos = userGameResultInfoManager.GetUserGameResultInfos();

        if(gameResultInfos.Count==0)
            return;

        this.SetTextData();

        this.InitializeCorrectBarData(this.gameResultInfos);

        for(int index = 0;index<this.correctBarDataList.Count;index++)
        {
            GameObject barObj =  GameObject.Instantiate(barPrefab);
            barObj.transform.SetParent(this.correctBarContentTransform,false);
            BarUIController barUIController = barObj.GetComponent<BarUIController>();

            barUIController.SetDateText(this.correctBarDataList[index].date);
            barUIController.SetBarSize(this.correctBarDataList[index].value/100);
        }

        this.InitializeBarData(this.gameResultInfos);
        this.SetStudyBarData();

    }

    private void SetTextData()
    {
        this.totalCountText.text = (this.gameResultInfos.Count * 8).ToString();

        int correctRateSum = 0;
        foreach(GameResultInfo resultInfo in this.gameResultInfos)
        {
            correctRateSum += resultInfo.progressData.explAcrcyRt;
        }
        correctRateSum /= this.gameResultInfos.Count;
        this.SetCorrectRateText(correctRateSum.ToString());
    }

    private void SetCorrectRateText(string rate)
    {
        this.totalRateText.text = $"{rate}%";
    }

    private void InitializeCorrectBarData(List<GameResultInfo> gameResultInfos)
    {

        this.correctBarDataList = new List<BarData>();

        int correctRateSum = 0;
        int dayCount = 0;
        string date = gameResultInfos[0].date.Substring(0,2);

        for(int index = 0;index<gameResultInfos.Count;index++)
        {
            if(date == gameResultInfos[index].date.Substring(0,2))
            {
                correctRateSum += gameResultInfos[index].progressData.explAcrcyRt;
                dayCount++;
            }else{
                if(dayCount != 0)
                    correctRateSum /= dayCount;
                string barDate = gameResultInfos[index].date.Substring(3,2);
                this.correctBarDataList.Add(new BarData(barDate,correctRateSum));
                
                date = gameResultInfos[index].date.Substring(0,2);
                dayCount = 0;

                correctRateSum = gameResultInfos[index].progressData.explAcrcyRt;
            }

            if(index == gameResultInfos.Count-1)
            {
                if(dayCount != 0)
                    correctRateSum /= dayCount;
                string barDate = gameResultInfos[index].date.Substring(3,2);
                this.correctBarDataList.Add(new BarData(barDate,correctRateSum));
            }
        
        }
    }

}

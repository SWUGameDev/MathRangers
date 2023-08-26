using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarData
{
    public string date;
    public int rate;

    public BarData(string date,int rate)
    {
        this.date = date;
        this.rate = rate;
    }
}

public class CorrectGraphUIPanelController : MonoBehaviour
{
    [SerializeField] private GameObject barPrefab;

    [SerializeField] private Transform barContentTransform;

    private List<BarData> barDataList;

    void Start()
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetCorrectGraphData;
        UserGameResultInfoManager.OnUserGameResultInfoInitialized += SetCorrectGraphData;  
    }
    private void SetCorrectGraphData(List<GameResultInfo> gameResultInfos)
    {

        if(gameResultInfos.Count==0)
            return;

        this.InitializeBarData(gameResultInfos);

        Debug.Log(barDataList.Count);

        for(int index = 0;index<this.barDataList.Count;index++)
        {
            GameObject barObj =  GameObject.Instantiate(barPrefab);
            barObj.transform.SetParent(this.barContentTransform,false);
            BarUIController barUIController = barObj.GetComponent<BarUIController>();

            barUIController.SetDateText(this.barDataList[index].date);
            barUIController.SetBarSize(this.barDataList[index].rate/100);
        }
    }

    private void InitializeBarData(List<GameResultInfo> gameResultInfos)
    {

        this.barDataList = new List<BarData>();

        int correctRateSum = 0;
        int dayCount = 0;
        string date = gameResultInfos[0].date.Substring(0,8);
        Debug.Log(date);

        for(int index = 0;index<gameResultInfos.Count;index++)
        {
            Debug.Log($"date:{date} , {gameResultInfos[index].date.Substring(0,8)}");
            if(date == gameResultInfos[index].date.Substring(0,8))
            {
                correctRateSum += gameResultInfos[index].progressData.explAcrcyRt;
                dayCount++;
            }else{
                if(dayCount != 0)
                    correctRateSum /= dayCount;
                string barDate = gameResultInfos[index].date.Substring(0,5);
                this.barDataList.Add(new BarData(barDate,correctRateSum));
                
                date = gameResultInfos[index].date.Substring(0,8);
                dayCount = 0;

                correctRateSum = gameResultInfos[index].progressData.explAcrcyRt;
            }

            if(index == gameResultInfos.Count-1)
            {
                if(dayCount != 0)
                    correctRateSum /= dayCount;
                string barDate = gameResultInfos[index].date.Substring(0,5);
                this.barDataList.Add(new BarData(barDate,correctRateSum));
            }
        
        }
    }


}

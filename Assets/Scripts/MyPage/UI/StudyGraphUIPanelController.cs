using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StudyGraphUIPanelController : MonoBehaviour
{
    [SerializeField] private GameObject barPrefab;

    [SerializeField] private Transform barContentTransform;

    [SerializeField] private TMP_Text maxValueText;

    [SerializeField] private TMP_Text midValueText;

    private List<BarData> barDataList;

    private int initMaxValueLevel = 2;

    private int maxValueUnit = 50;

    private float maxValue = 100;

    private int maxQuestionCount = 8;

    void Start()
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetStudyGraphData;
        UserGameResultInfoManager.OnUserGameResultInfoInitialized += SetStudyGraphData;  
    }

    private void OnDestroy() {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetStudyGraphData;
    }
    private void SetStudyGraphData(List<GameResultInfo> gameResultInfos)
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetStudyGraphData;

        if(gameResultInfos.Count==0)
            return;

        if(gameResultInfos.Count>7)
        {
            gameResultInfos.RemoveRange(0,gameResultInfos.Count-7);
        }

        this.InitializeBarData(gameResultInfos);

        if(this.barDataList.Count == 0)
        {
            int maxLevel = this.barDataList[0].value % this.maxValueUnit;
            
            if(maxLevel > this.initMaxValueLevel)
            {
                this.maxValue = maxLevel * this.maxValueUnit;
                this.maxValueText.text = $"- {this.maxValue.ToString()}";
                this.midValueText.text = $"- {(this.maxValue/2).ToString()}";
            }

            GameObject barObj =  GameObject.Instantiate(barPrefab);
            barObj.transform.SetParent(this.barContentTransform,false);
            BarUIController barUIController = barObj.GetComponent<BarUIController>();

            barUIController.SetDateText(this.barDataList[0].date);
            barUIController.SetBarSize(this.barDataList[0].value/this.maxValue);

        }else
        {
            int maxValueInList = 100;
            for(int index = 0;index<this.barDataList.Count;index++)
            {
                if(maxValueInList<this.barDataList[index].value)
                {
                    maxValueInList = this.barDataList[index].value;
                }
            }

            this.maxValue = maxValueInList;
            this.maxValueText.text = $"- {this.maxValue.ToString()}";
            this.midValueText.text = $"- {(this.maxValue/2).ToString()}";

            for(int index = 0;index<this.barDataList.Count;index++)
            {
                GameObject barObj =  GameObject.Instantiate(barPrefab);
                barObj.transform.SetParent(this.barContentTransform,false);
                BarUIController barUIController = barObj.GetComponent<BarUIController>();

                barUIController.SetDateText(this.barDataList[index].date);
                barUIController.SetBarSize(this.barDataList[index].value/this.maxValue);
            }

        }


    }

    private void InitializeBarData(List<GameResultInfo> gameResultInfos)
    {

        this.barDataList = new List<BarData>();

        int studyCountSum = 0;
        int dayCount = 0;
        string date = gameResultInfos[0].date.Substring(0,8);

        for(int index = 0;index<gameResultInfos.Count;index++)
        {
            Debug.Log(gameResultInfos[index].date);
            if(date == gameResultInfos[index].date.Substring(0,8))
            {
                studyCountSum += this.maxQuestionCount;
                dayCount++;
            }else{
                string barDate = gameResultInfos[index-1].date.Substring(3,5);
                this.barDataList.Add(new BarData(barDate,studyCountSum));
                
                date = gameResultInfos[index].date.Substring(0,8);
                dayCount = 0;

                studyCountSum = this.maxQuestionCount;
            }

            if(index == gameResultInfos.Count-1)
            {
                string barDate = gameResultInfos[index].date.Substring(3,5);
                this.barDataList.Add(new BarData(barDate,studyCountSum));
            }
        
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public partial class TotalRecordController : MonoBehaviour
{

    [SerializeField] private List<BarData> studyBarDataList;
    private int initMaxValueLevel = 2;

    private int maxValueUnit = 50;

    private float maxValue = 100;
    private int maxQuestionCount = 8;

    [SerializeField] private Transform studyBarContentTransform;

    [SerializeField] private TMP_Text maxValueText;

    [SerializeField] private TMP_Text midValueText;
    private void InitializeBarData(List<GameResultInfo> gameResultInfos)
    {

        this.studyBarDataList = new List<BarData>();

        int studyCountSum = 0;
        int dayCount = 0;
        string date = gameResultInfos[0].date.Substring(0,2);

        for(int index = 0;index<gameResultInfos.Count;index++)
        {
            if(date == gameResultInfos[index].date.Substring(0,2))
            {
                studyCountSum += this.maxQuestionCount;
                dayCount++;
            }else{
                string barDate = gameResultInfos[index].date.Substring(3,2);
                this.studyBarDataList.Add(new BarData(barDate,studyCountSum));
                
                date = gameResultInfos[index].date.Substring(0,2);
                dayCount = 0;

                studyCountSum = this.maxQuestionCount;
            }

            if(index == gameResultInfos.Count-1)
            {
                string barDate = gameResultInfos[index].date.Substring(3,2);
                this.studyBarDataList.Add(new BarData(barDate,studyCountSum));
            }
        
        }
    }

    private void SetStudyBarData()
    {
        if(this.studyBarDataList.Count == 0)
        {
            int maxLevel = this.studyBarDataList[0].value % this.maxValueUnit;
            
            if(maxLevel > this.initMaxValueLevel)
            {
                this.maxValue = maxLevel * this.maxValueUnit;
                this.maxValueText.text = $"- {this.maxValue.ToString()}";
                this.midValueText.text = $"- {(this.maxValue/2).ToString()}";
            }

            GameObject barObj =  GameObject.Instantiate(barPrefab);
            barObj.transform.SetParent(this.studyBarContentTransform,false);
            BarUIController barUIController = barObj.GetComponent<BarUIController>();

            barUIController.SetDateText(this.studyBarDataList[0].date);
            barUIController.SetBarSize(this.studyBarDataList[0].value/this.maxValue);

        }else
        {
            int maxValueInList = 100;
            for(int index = 0;index<this.studyBarDataList.Count;index++)
            {
                if(maxValueInList<this.studyBarDataList[index].value)
                {
                    maxValueInList = this.studyBarDataList[index].value;
                }
            }

            this.maxValue = maxValueInList;
            this.maxValueText.text = $"- {this.maxValue.ToString()}";
            this.midValueText.text = $"- {(this.maxValue/2).ToString()}";

            for(int index = 0;index<this.studyBarDataList.Count;index++)
            {
                GameObject barObj =  GameObject.Instantiate(barPrefab);
                barObj.transform.SetParent(this.studyBarContentTransform,false);
                BarUIController barUIController = barObj.GetComponent<BarUIController>();

                barUIController.SetDateText(this.studyBarDataList[index].date);
                barUIController.SetBarSize(this.studyBarDataList[index].value/this.maxValue);
            }

        }
    }
}

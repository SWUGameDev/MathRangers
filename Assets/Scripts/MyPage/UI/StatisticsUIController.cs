using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsUIController : MonoBehaviour
{
    [SerializeField] private UserGameResultInfoManager userGameResultInfoManager;


    private List<GameResultInfo> gameResultInfos;

    [SerializeField] private Transform statisticsContentTransform;

    [SerializeField] private GameObject infoPrefab;

    void Start()
    {
        this.gameResultInfos = userGameResultInfoManager.GetUserGameResultInfos();

        this.gameResultInfos.Reverse();
        
        if(gameResultInfos.Count==0)
            return;

        for(int index = 0;index<this.gameResultInfos.Count;index++)
        {
            GameObject infoObj =  GameObject.Instantiate(infoPrefab);
            infoObj.transform.SetParent(this.statisticsContentTransform,false);
            GameResultInfoUIController barUIController = infoObj.GetComponent<GameResultInfoUIController>();
            string[] infos = new string[7];

            infos[0] = this.gameResultInfos[index].date;
            infos[1] = this.gameResultInfos[index].gameResult.damage.ToString("N0");

            switch( this.gameResultInfos[index].progressData.acrcyCd)
            {
            case "A":
                infos[2] = "완벽";
                break;
            case "B":
                infos[2] = "높음";
                break;
            case "C":
                infos[2] = "보통";
                break;
            case "D":
                infos[2] = "미달";
                break;
            default:
                infos[2] = "None";
                break;
            }

        switch(this.gameResultInfos[index].progressData.explSpedCd)
        {
            case "ESC01":
                infos[3] = "느림";
                break;
            case "ESC02":
                infos[3] = "보통";
                break;
            case "ESC03":
                infos[3] = "빠름";
                break;
            default:
                infos[3] = "None";
                break;
        }

  
        switch(this.gameResultInfos[index].progressData.lrnPrgsStsCd)
        {
            case "LPSC01":
                infos[4] = "노력";
                break;
            case "LPSC02":
                infos[4] = "기본";
                break;
            case "LPSC03":
                infos[4] = "충분";
                break;
            case "LPSC04":
                infos[4] = "훌륭";
                break;
            default:
                infos[4] = "None";
                break;
        }     

        infos[5] =  $"{this.gameResultInfos[index].progressData.explAcrcyRt}%";

        switch(this.gameResultInfos[index].gameResultType)
        {
            case (int)GameResultType.EmergencyAbortOfMission:
                infos[6] = "긴급중단";
                break;
            case (int)GameResultType.MissionFail:
                infos[6] = "임무실패";
                break;
            case (int)GameResultType.MissionSuccess:
                infos[6] = "임무성공";
                break;
            default:
                infos[6] = "None";
                break;
        }    

        barUIController.SetText(infos);
        }        

    }

}

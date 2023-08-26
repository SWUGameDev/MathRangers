using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WjChallenge;

[Serializable]
public class GameResultInfo 
{
    public string date;

    public GameResultData gameResult;

    public Response_Learning_ProgressData progressData;

    public int gameResultType;
    public GameResultInfo(GameResultType type,GameResultData gameResultData,Response_Learning_ProgressData progressData)
    {
        this.date =  DateTime.Now.ToString("yy.MM.dd hh:mm tt");

        this.gameResult = gameResultData;

        this.gameResultType = (int)type;

        this.progressData = progressData;

    }
    public GameResultInfo()
    {

    }
}

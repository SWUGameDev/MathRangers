using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGameResultInfoManager : MonoBehaviour
{

    private List<GameResultInfo> userGameResultInfos;

    public static Action<List<GameResultInfo>> OnUserGameResultInfoInitialized;

    void Start()
    {
        string userId = FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserId();

        FirebaseRealtimeDatabaseManager.Instance.LoadGameResultInfoList(userId,this.InitUserGameResultInfoData);
    }

    public List<GameResultInfo> GetUserGameResultInfos()
    {
        return this.userGameResultInfos;
    }

    private void InitUserGameResultInfoData(List<GameResultInfo> gameResultInfos)
    {
        this.userGameResultInfos = gameResultInfos;

        UserGameResultInfoManager.OnUserGameResultInfoInitialized?.Invoke(this.userGameResultInfos);
    }

}

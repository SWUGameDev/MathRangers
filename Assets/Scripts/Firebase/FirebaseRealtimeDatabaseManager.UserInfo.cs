using System;
using System.Collections;
using System.Collections.Generic;
public partial class FirebaseRealtimeDatabaseManager 
{
    public void UploadUserInfo(string userUID, string serializedUserInfo,Action OnCompleted = null)
    {
        this.WriteData<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",serializedUserInfo,OnCompleted);
    }

    public void UploadGameResultInfo(string userUID, string serializedGameResultInfo, Action OnCompleted = null)
    {
        this.WriteData<GameResultInfo>($"{FirebaseRealtimeDatabaseManager.gameResultInfoRootKey}/{userUID}/{DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss")}", serializedGameResultInfo, OnCompleted);
    }

    public void UploadInitializedUserRankInfo(string userUID, string serializedUserRankInfo, Action OnCompleted = null)
    {
        this.WriteData<UserRankInfo>($"{FirebaseRealtimeDatabaseManager.rankInfoRootKey}/{userUID}", serializedUserRankInfo, OnCompleted);
    }

    public void UpdateUserScoreInfo(string userUID, string score, Action OnCompleted = null)
    {
        this.WriteData<UserRankInfo>($"{FirebaseRealtimeDatabaseManager.rankInfoRootKey}/{userUID}/score", score, OnCompleted);
    }

    public void UpdateUserIconInfo(string team, Action OnCompleted = null)
    {
        string userUID = this.GetCurrentUserId();
        this.WriteData<UserRankInfo>($"{FirebaseRealtimeDatabaseManager.rankInfoRootKey}/{userUID}/team", team, OnCompleted);
    }

    public void UploadInitializedUserInfo(string userUID, string serializedUserInfo,Action OnCompleted = null)
    {
        this.WriteDataUsingMainTread<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",serializedUserInfo,OnCompleted);
    }

    public void LoadUserInfo(string userUID,Action<UserInfo> onCompleted = null)
    {
        this.ReadData<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",onCompleted);
    }

    public void LoadGameResultInfoList(string userUID,Action<List<GameResultInfo>> onCompleted = null)
    {
        this.ReadDataList<GameResultInfo>($"{FirebaseRealtimeDatabaseManager.gameResultInfoRootKey}/{userUID}",onCompleted);
    }

}

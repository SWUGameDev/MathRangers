using System;
public partial class FirebaseRealtimeDatabaseManager 
{
    public void UploadUserInfo(string userUID, string serializedUserInfo,Action OnCompleted = null)
    {
        this.WriteData<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",serializedUserInfo,OnCompleted);
    }

    public void UploadGameResultInfo(string userUID, string serializedGameResultInfo, Action OnCompleted = null)
    {
        this.WriteData<UserInfo>($"{FirebaseRealtimeDatabaseManager.gameResultInfoRootKey}/{userUID}/{DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss")}", serializedGameResultInfo, OnCompleted);
    }


    public void UploadInitializedUserInfo(string userUID, string serializedUserInfo,Action OnCompleted = null)
    {
        this.WriteDataUsingMainTread<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",serializedUserInfo,OnCompleted);
    }

    public void LoadUserInfo(string userUID,Action<UserInfo> onCompleted = null)
    {
        this.ReadData<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",onCompleted);
    }

}

using System;
public partial class FirebaseRealtimeDatabaseManager 
{
    public void UploadUserInfo(string userUID, string serializedUserInfo,Action OnCompleted = null)
    {
        this.WriteData<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",serializedUserInfo,OnCompleted);
    }

    public void LoadUserInfo(string userUID,Action<UserInfo> onCompleted = null)
    {
        this.ReadData<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",onCompleted);
    }

}

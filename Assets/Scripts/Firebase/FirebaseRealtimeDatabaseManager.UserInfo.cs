using System;
public partial class FirebaseRealtimeDatabaseManager 
{
    public void UploadUserInfo(string userUID, string serializedUserInfo)
    {
        this.WriteData<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",serializedUserInfo);
    }

    public void LoadUserInfo(string userUID,Action<UserInfo> onCompleted)
    {
        this.ReadData<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",onCompleted);
    }

}

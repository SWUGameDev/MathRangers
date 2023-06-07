
public partial class FirebaseRealtimeDatabaseManager 
{
    public void UploadUserInfo(string userUID, string serializedUserInfo)
    {
        this.WriteData<UserInfo>($"{FirebaseRealtimeDatabaseManager.userInfoRootKey}/{userUID}",serializedUserInfo);
    }

    public UserInfo LoadUserInfo(string userUID)
    {
        return new UserInfo();
    }

}

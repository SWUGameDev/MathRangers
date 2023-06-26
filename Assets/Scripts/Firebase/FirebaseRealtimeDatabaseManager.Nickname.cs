using System;
using Firebase.Database;
public partial class FirebaseRealtimeDatabaseManager 
{
    public void CheckDuplicateNickname(string nickname,Action OnFailed = null,Action<string> OnIsDuplicated = null ,Action<string> OnIsNotDuplicated = null)
    {
        this.CheckDuplicatedValue("UserInfo","nickname",nickname,OnFailed,OnIsDuplicated,OnIsNotDuplicated);
    }

}

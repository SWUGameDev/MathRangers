using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FirebaseDatabaseTest : MonoBehaviour
{
    [MenuItem("Firebase/Logout")]
    static void LogOut()
    {
        FirebaseRealtimeDatabaseManager.Instance.Logout();
    }

    [MenuItem("Firebase/Upload")]
    static void UploadTest()
    {
        UserInfo userInfo = new UserInfo("test@naver.com","MyName");
        string serializedData = JsonUtility.ToJson(userInfo);
        FirebaseRealtimeDatabaseManager.Instance.UploadUserInfo("YGU345D2sD",serializedData);
    }

    [MenuItem("Firebase/Download")]
    static void DownloadTest()
    {
        FirebaseRealtimeDatabaseManager.Instance.LoadUserInfo("YGU345D2sD",(info)=>{Debug.Log(info.email);});
    }

}

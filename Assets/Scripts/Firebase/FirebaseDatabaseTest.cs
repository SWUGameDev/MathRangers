using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseDatabaseTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UserInfo userInfo = new UserInfo("test@naver.com","MyName");
        string serializedData = JsonUtility.ToJson(userInfo);
        FirebaseRealtimeDatabaseManager.Instance.UploadUserInfo("YGU345D2sD",serializedData);
    }

}

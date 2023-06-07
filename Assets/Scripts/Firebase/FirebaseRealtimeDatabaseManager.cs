using System;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;

public partial class FirebaseRealtimeDatabaseManager 
{
    private DatabaseReference databaseReference;

    private static readonly Lazy<FirebaseRealtimeDatabaseManager> _instance = new Lazy<FirebaseRealtimeDatabaseManager>(()=> new FirebaseRealtimeDatabaseManager());

    public static FirebaseRealtimeDatabaseManager Instance { get { return _instance.Value; } }

    private static readonly string userInfoRootKey = "UserInfo";

    private FirebaseRealtimeDatabaseManager()
    {
        this.databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void WriteData<T>(string key, string value)
    {
        this.databaseReference.Child(key).SetRawJsonValueAsync(value).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Data write encountered an error: " + task.Exception);
            }else if(task.IsCompleted)
            {
                Debug.Log("Upload Complete");
            }
        });
    }

}

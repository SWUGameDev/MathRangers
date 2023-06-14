using System;
using UnityEngine;
using Firebase.Database;
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

    private async void ReadData<T>(string key,Action<T> action)
    {
        try
        {
            DataSnapshot snapshot = await databaseReference.Child(key).GetValueAsync();

            if (snapshot != null && snapshot.Exists)
            {
                string deserializedData = snapshot.GetRawJsonValue();
                T data =  JsonUtility.FromJson<T>(deserializedData);
                action.Invoke(data);
            }
            else
            {
                Debug.Log("Data not found");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error retrieving data: " + e.Message);
        }
    }

    private async void FetchScoresByOrder(string rootKey,string chidKey,int limitCount,Action<DataSnapshot> OnCompleted)
    {
        DatabaseReference scoresRef = databaseReference.Child(rootKey);
        Query query = scoresRef.OrderByChild(chidKey).LimitToFirst(limitCount);

        DataSnapshot snapshot = await query.GetValueAsync();

        if (snapshot != null && snapshot.HasChildren)
        {
            OnCompleted.Invoke(snapshot);
        }
    }

}


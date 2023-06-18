using System;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
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

    public string GetCurrentUserId()
    {
        FirebaseUser currentUser = FirebaseAuth.DefaultInstance.CurrentUser;
        return currentUser.UserId;
    }

    private void RefreshReferenceKeepSynced(string referenceName) {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(referenceName);
        if(reference == null)
            return;
        reference.KeepSynced(false);
        reference.KeepSynced(true);
    }

    private void WriteData<T>(string key, string value,Action onCompleted = null)
    {
        this.databaseReference.Child(key).SetRawJsonValueAsync(value).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Data write encountered an error: " + task.Exception);
            }else if(task.IsCompleted)
            {
                Debug.Log("Upload Complete");

                onCompleted?.Invoke();
            }
        });
    }

    private void WriteDataUsingMainTread<T>(string key, string value,Action onCompleted = null)
    {
        this.databaseReference.Child(key).SetRawJsonValueAsync(value).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Data write encountered an error: " + task.Exception);
            }else if(task.IsCompleted)
            {
                Debug.Log("Upload Complete");
                onCompleted?.Invoke();
            }
        },TaskScheduler.FromCurrentSynchronizationContext());
    }

    private async void ReadData<T>(string key,Action<T> OnCompleted = null)
    {
        try
        {
            DataSnapshot snapshot = await databaseReference.Child(key).GetValueAsync();

            if (snapshot != null && snapshot.Exists)
            {
                string deserializedData = snapshot.GetRawJsonValue();
                T data =  JsonUtility.FromJson<T>(deserializedData);
                OnCompleted?.Invoke(data);
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

    private async void FetchScoresByOrder(string rootKey,string chidKey,int limitCount,Action<DataSnapshot> OnCompleted = null)
    {
        DatabaseReference scoresRef = databaseReference.Child(rootKey);

        DataSnapshot snapshot = await scoresRef.GetValueAsync();

        if (snapshot != null && snapshot.HasChildren)
        {
            OnCompleted?.Invoke(snapshot);
        }
    }

}


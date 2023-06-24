using System;
using Firebase.Database;
public partial class FirebaseRealtimeDatabaseManager 
{
    public void LoadUserIndividualRankingInfo(int limitCount,Action<DataSnapshot> onCompleted)
    {
        this.RefreshReferenceKeepSynced("Rank");

        this.FetchScoresByOrder("Rank","score",limitCount,onCompleted);
    }

}
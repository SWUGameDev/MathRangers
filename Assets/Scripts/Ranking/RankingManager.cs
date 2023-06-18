using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using TeamName = System.String;
using totalScore = System.Int64;
using System.Linq;

public partial class RankingManager : MonoBehaviour
{
    [SerializeField] private int itemCount;

    [SerializeField] private RankingUIManager rankingUIManager;

    private List<UserRankInfo> infos;

    private void Awake() {
        this.LoadRankingData();
    }

    private void LoadRankingData()
    {
        FirebaseRealtimeDatabaseManager.Instance.LoadUserIndividualRankingInfo(10,this.GetInfo);
    }

    private void GetInfo(DataSnapshot dataSnapshot)
    {
        this.infos = new List<UserRankInfo>();
        foreach (var childSnapshot in dataSnapshot.Children)
        {
            string json = childSnapshot.GetRawJsonValue();
            UserRankInfo score = JsonUtility.FromJson<UserRankInfo>(json);
            infos.Add(score);
        }
        this.SetUISettings();
    }

    private void SetUISettings()
    {
        this.rankingUIManager.SetTeamRankUI(this.CalculateTotalSumByTeams());

        this.rankingUIManager.CreatePersonalRankingItem(this.OrderByUserScore());
    }

}

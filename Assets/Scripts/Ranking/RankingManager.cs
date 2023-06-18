using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using TeamName = System.String;
using totalScore = System.Int64;
using System.Linq;

public partial class RankingManager : MonoBehaviour
{
    [SerializeField] private GameObject rankItemPrefab;

    [SerializeField] private int itemCount;

    private List<RankingUIItem> rankingItems;


    [SerializeField] private Transform personalContentParentTransform;

    [SerializeField] private Color[] rankBackgroundColors;

    [SerializeField] private Color myRankHighlightBackgroundColor;

    [SerializeField] private RankingUIManager rankingUIManager;


    private void Awake() {
        this.LoadRankingData();
    }

    private void LoadRankingData()
    {
        FirebaseRealtimeDatabaseManager.Instance.LoadUserIndividualRankingInfo(10,this.GetInfo);
    }

    private void GetInfo(DataSnapshot dataSnapshot)
    {
        List<UserRankInfo> infos = new List<UserRankInfo>();
        foreach (var childSnapshot in dataSnapshot.Children)
        {
            string json = childSnapshot.GetRawJsonValue();
            UserRankInfo score = JsonUtility.FromJson<UserRankInfo>(json);
            infos.Add(score);
        }
        this.InitializeRankingDataByInfos(infos);
    }

    private void InitializeRankingDataByInfos(List<UserRankInfo> infos)
    {
        this.OrderByUserScore(infos);
        this.CalculateTotalSumByTeams(infos);
    }

}

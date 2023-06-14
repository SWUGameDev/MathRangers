using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    [SerializeField] private GameObject rankItemPrefab;

    [SerializeField] private int itemCount;

    private List<RankingItemController> rankingItemControllers;


    private void Awake() {
        this.LoadRankingData();
    }

    private void LoadRankingData()
    {
        FirebaseRealtimeDatabaseManager.Instance.LoadUserIndividualRankingInfo(10,this.GetInfo);
    }

    private void GetInfo(DataSnapshot dataSnapshot)
    {
        foreach (var childSnapshot in dataSnapshot.Children)
        {
            // 각 데이터 항목에 대한 처리
            Debug.Log(childSnapshot.Key + ": " + childSnapshot.Value);
            // JSON 문자열로 변환
            string json = childSnapshot.GetRawJsonValue();

            // 클래스로 역직렬화
            UserRankInfo score = JsonUtility.FromJson<UserRankInfo>(json);

            // 데이터 처리
            Debug.Log(score.nickname + ": " + score.score);
        }
    }
}

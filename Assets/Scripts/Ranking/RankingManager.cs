using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    [SerializeField] private GameObject rankItemPrefab;

    [SerializeField] private int itemCount;

    private List<RankingItemController> rankingItemControllers;


    [SerializeField] private Transform[] contentParentTransforms;

    [SerializeField] private Color[] rankBackgroundColors;

    [SerializeField] private Color myRankHighlightBackgroundColor;


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
        infos.Sort((x, y) => y.score.CompareTo(x.score));
        this.CreateRankingItem(infos);
    }

    private void CreateRankingItem(List<UserRankInfo> infos)
    {
        Transform parent = this.contentParentTransforms[1];
        this.rankingItemControllers = new List<RankingItemController>();

        for(int index = 0;index<infos.Count;index++)
        {
            GameObject item = GameObject.Instantiate<GameObject>(this.rankItemPrefab);
            item.transform.SetParent(this.contentParentTransforms[1]);
            RankingItemController controller = item.GetComponent<RankingItemController>();

            controller.InitializeRankingItemController(index+1,infos[index]);
            this.SetRankBackgroundColor(index,controller);
            this.SetMyRankBackgroundColor(infos[index],controller);

            this.rankingItemControllers.Add(controller) ;
        }
    }

    private void SetRankBackgroundColor(int index,RankingItemController rankingItemController)
    {
        if(index>2)
            return;
        
        rankingItemController.SetRankBackGroundImage(this.rankBackgroundColors[index]);
    }

    private void SetMyRankBackgroundColor(UserRankInfo rankInfo,RankingItemController rankingItemController)
    {
        return;

        //rankingItemController.SetRankBackGroundImage(this.myRankHighlightBackgroundColor);
    }
}

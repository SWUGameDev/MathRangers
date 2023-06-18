using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using TeamName = System.String;
using totalScore = System.Int64;
using System.Linq;

public partial class RankingManager : MonoBehaviour
{

    private void OrderByUserScore(List<UserRankInfo> infos)
    {
        infos.Sort((x, y) => y.score.CompareTo(x.score));
        this.CreateRankingItem(infos);
    }

    private void CreateRankingItem(List<UserRankInfo> infos)
    {
        Transform parent = this.personalContentParentTransform;
        this.rankingItems = new List<RankingUIItem>();

        for(int index = 0;index<infos.Count;index++)
        {
            GameObject item = GameObject.Instantiate<GameObject>(this.rankItemPrefab);
            item.transform.SetParent(this.personalContentParentTransform);
            RankingUIItem controller = item.GetComponent<RankingUIItem>();

            controller.InitializeRankingItemController(index+1,infos[index]);
            this.SetRankBackgroundColor(index,controller);
            this.SetMyRankBackgroundColor(infos[index],controller);

            this.rankingItems.Add(controller) ;
        }
    }

    private void SetRankBackgroundColor(int index,RankingUIItem rankingItemController)
    {
        if(index>2)
            return;
        
        rankingItemController.SetRankBackGroundImage(this.rankBackgroundColors[index]);
    }

    private void SetMyRankBackgroundColor(UserRankInfo rankInfo,RankingUIItem rankingItemController)
    {
        return;

        //rankingItemController.SetRankBackGroundImage(this.myRankHighlightBackgroundColor);
    }
}

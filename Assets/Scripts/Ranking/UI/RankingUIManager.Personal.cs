using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;


public partial class RankingUIManager : MonoBehaviour
{
    [Header("Personal Tab UI")]

    [SerializeField] private GameObject rankItemPrefab;

    private List<RankingUIItem> rankingItems;

    [SerializeField] private Transform personalContentParentTransform;

    [SerializeField] private Color[] rankBackgroundColors;

    [SerializeField] private Color myRankHighlightBackgroundColor;

    public bool CreatePersonalRankingItem(List<UserRankInfo> infos)
    {
        if(infos == null)
            return false;

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

        return true;
    }

    private void SetRankBackgroundColor(int index,RankingUIItem rankingItemController)
    {
        if(index>2)
            return;
        
        rankingItemController.SetRankBackGroundImage(this.rankBackgroundColors[index]);
    }

    private void SetMyRankBackgroundColor(UserRankInfo rankInfo,RankingUIItem rankingItemController)
    {
        string userId = FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserId();
        
        if(userId == null)
            return;

        if(userId == rankInfo.UID)
            rankingItemController.SetItemBackGroundImage(this.myRankHighlightBackgroundColor);
    }
    
}

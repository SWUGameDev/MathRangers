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

    private string userId = ""; 

    private bool isCurrentUserChecked = false;

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
            if(this.isCurrentUserChecked == false)
                this.SetMyRankBackgroundColor(infos[index],controller);

            this.rankingItems.Add(controller);
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
        if(this.userId == "")
            this.userId = FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserId();
        
        if(this.userId == null)
            return;

        if(this.userId == rankInfo.UID)
        {
            rankingItemController.SetMyRankBackGroundActive(true);
            this.isCurrentUserChecked = true;
        }
        
    }
    
}

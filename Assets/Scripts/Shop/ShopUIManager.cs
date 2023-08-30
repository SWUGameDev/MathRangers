using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public partial class ShopUIManager : MonoBehaviour
{
    private Dictionary<ItemType,ItemUIInfo> selectedItemInfo;

    [SerializeField] private GameObject purchaseButton; 

    [SerializeField] private GameObject applyButton; 

    public static Action<ItemInfo,bool> OnItemSelected;

    public void SelectItem(ItemUIInfo itemUIInfo)
    {
        if(this.selectedItemInfo == null)
            this.selectedItemInfo = new Dictionary<ItemType, ItemUIInfo>();

        if(this.selectedItemInfo.ContainsKey(itemUIInfo.itemInfo.itemType))
        {
            if(this.selectedItemInfo[itemUIInfo.itemInfo.itemType] == itemUIInfo)
            {
                this.selectedItemInfo[itemUIInfo.itemInfo.itemType].SetSelectedPanel(false);
                this.selectedItemInfo.Remove(itemUIInfo.itemInfo.itemType);
                ShopUIManager.OnItemSelected?.Invoke(itemUIInfo.itemInfo,false);
            }else{
                this.selectedItemInfo[itemUIInfo.itemInfo.itemType].SetSelectedPanel(false);
                this.selectedItemInfo[itemUIInfo.itemInfo.itemType] = itemUIInfo;
                this.selectedItemInfo[itemUIInfo.itemInfo.itemType].SetSelectedPanel(true);
                ShopUIManager.OnItemSelected?.Invoke(itemUIInfo.itemInfo,true);
            }
        }else{
            this.selectedItemInfo[itemUIInfo.itemInfo.itemType] = itemUIInfo;
            this.selectedItemInfo[itemUIInfo.itemInfo.itemType].SetSelectedPanel(true);
            ShopUIManager.OnItemSelected?.Invoke(itemUIInfo.itemInfo,true);
        }

        this.CheckButtonCanActive();
        
    }

    private void CheckButtonCanActive()
    {
        if(this.selectedItemInfo.Count == 0)
        {
            this.purchaseButton.SetActive(false);
            this.applyButton.SetActive(false);
        }
        else
        {
            if(this.IsAllSelectedItemOwned())
            {
                this.applyButton.SetActive(true);
                this.purchaseButton.SetActive(false);
            }else{
                this.applyButton.SetActive(false);
                this.purchaseButton.SetActive(true);
            }
            
        }
    }

    private bool IsAllSelectedItemOwned()
    {
        foreach(KeyValuePair<ItemType,ItemUIInfo> info in this.selectedItemInfo)
        {
            if(info.Value.itemInfo.isOwned == false)
                return false;
        }
        return true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public partial class ShopUIManager : MonoBehaviour
{
    private Dictionary<ItemType,ItemUIInfo> selectedItemInfo;

    [SerializeField] private GameObject purchaseButton; 

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

        this.CheckPurchaseButtonCanActive();
        
    }

    private void CheckPurchaseButtonCanActive()
    {
        if(this.selectedItemInfo.Count == 0)
            this.purchaseButton.SetActive(false);
        else
            this.purchaseButton.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopUIManager : MonoBehaviour
{
    private Dictionary<ItemType,ItemUIInfo> selectedItemInfo;

    [SerializeField] private GameObject purchaseButton; 

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
            }else{
                this.selectedItemInfo[itemUIInfo.itemInfo.itemType].SetSelectedPanel(false);
                this.selectedItemInfo[itemUIInfo.itemInfo.itemType] = itemUIInfo;
                this.selectedItemInfo[itemUIInfo.itemInfo.itemType].SetSelectedPanel(true);
            }
        }else{
            this.selectedItemInfo[itemUIInfo.itemInfo.itemType] = itemUIInfo;
            this.selectedItemInfo[itemUIInfo.itemInfo.itemType].SetSelectedPanel(true);
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

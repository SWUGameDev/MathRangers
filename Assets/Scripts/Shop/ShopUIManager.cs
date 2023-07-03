using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopUIManager : MonoBehaviour
{
    private Dictionary<ItemType,ItemUIInfo> selectedItemInfo;

    public void SelectItem(ItemUIInfo itemUIInfo)
    {
        if(this.selectedItemInfo == null)
            this.selectedItemInfo = new Dictionary<ItemType, ItemUIInfo>();

        if(this.selectedItemInfo.ContainsKey(itemUIInfo.itemInfo.itemType))
            this.selectedItemInfo[itemUIInfo.itemInfo.itemType].SetSelectedPanel(false);

        this.selectedItemInfo[itemUIInfo.itemInfo.itemType] = itemUIInfo;
        this.selectedItemInfo[itemUIInfo.itemInfo.itemType].SetSelectedPanel(true);

        
    }
}

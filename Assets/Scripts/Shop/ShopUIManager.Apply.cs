using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
public partial class ShopUIManager : MonoBehaviour
{
    public void ClickApplyButton()
    {
        string serializedData = PlayerPrefManager.GetString(PlayerPrefManager.PlayerItemSetDictionaryKey);
        Dictionary<int,string> isItemSetDictionary;
        if(serializedData == "")
        {
            isItemSetDictionary = new Dictionary<int, string>();
        }else{
            isItemSetDictionary = JsonConvert.DeserializeObject<Dictionary<int,string>>(serializedData);
        }
            foreach(KeyValuePair<ItemType,ItemUIInfo> info in this.selectedItemInfo)
            {
                isItemSetDictionary[(int)info.Value.itemInfo.itemType] = info.Value.itemInfo.itemResourceFileName;
            }

        PlayerPrefManager.SetString(PlayerPrefManager.PlayerItemSetDictionaryKey,JsonConvert.SerializeObject(isItemSetDictionary));
    
        this.localizationIndex = LocalizationManager.Instance.GetCurrentLocalizationIndex();

            if(this.localizationIndex==1)
                this.noticeMessageUIManager.PopUpMessage("멋지게 꾸몄어쮸! :D",new Vector2(1000,300), new Color(89/255,151/255,60/255,1));
            else
                this.noticeMessageUIManager.PopUpMessage("Dressed nicely! :D",new Vector2(1000,400), new Color(89/255,151/255,60/255,1));

    }
}

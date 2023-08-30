using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
public partial class ShopUIManager : MonoBehaviour
{
    [SerializeField] private NoticeMessageUIManager noticeMessageUIManager;

    private int localizationIndex = 0;

    private Dictionary<int,bool> isInitOwnedDataDictionary; // Set 으로 할껄 ....

    private void Awake()
    {
        string serializedData = PlayerPrefManager.GetString(PlayerPrefManager.PlayerItemOwnedKey);
        if(serializedData == "")
        {
            this.isInitOwnedDataDictionary = new Dictionary<int, bool>();
        }else{
            this.isInitOwnedDataDictionary = JsonConvert.DeserializeObject<Dictionary<int,bool>>(serializedData);
        }
    }

    public bool IsOwnedItem(int itemId)
    {
        if(this.isInitOwnedDataDictionary.ContainsKey(itemId)==false)
            return false;

        return this.isInitOwnedDataDictionary[itemId];
    }

    
    public void ClickPurchaseButton()
    {
        this.localizationIndex = LocalizationManager.Instance.GetCurrentLocalizationIndex();

        if(this.IsContainLockedItem())
        {
            if(this.localizationIndex==1)
                this.noticeMessageUIManager.PopUpMessage("선택한 물품 중에 아직 살 수 없는 레벨의 물품이 있어쮸!",new Vector2(1000,300));
            else
                this.noticeMessageUIManager.PopUpMessage("There are some items you can't buy because of your level among the items you chose!",new Vector2(1000,400));
            return;
        }

        this.CalculateAllItemPrice();

        
    }

    // 나중에 절대 분리하기
    private void CalculateAllItemPrice()
    {
        int price = 0;
        foreach(KeyValuePair<ItemType,ItemUIInfo> info in this.selectedItemInfo)
        {
            if(info.Value.itemInfo.isOwned)
                continue;

            price += info.Value.itemInfo.price;
        }
        
        int playerMoney = PlayerPrefManager.GetInt(PlayerPrefManager.GameMoneyKey);

        if(playerMoney-price >= 0)
        {
            playerMoney = playerMoney-price;
            PlayerPrefManager.SetInt(PlayerPrefManager.GameMoneyKey,playerMoney);

            //데이터 저장 처리
            string serializedData = PlayerPrefManager.GetString(PlayerPrefManager.PlayerItemOwnedKey);
            Dictionary<int,bool> isOwnedList;

            if(serializedData == "")
            {
                isOwnedList = new Dictionary<int, bool>();
            }else{
                isOwnedList = JsonConvert.DeserializeObject<Dictionary<int,bool>>(serializedData);
            }

            foreach(KeyValuePair<ItemType,ItemUIInfo> info in this.selectedItemInfo)
            {
                if(info.Value.itemInfo.isOwned == false)
                {
                    info.Value.itemInfo.isOwned = true;
                    isOwnedList[info.Value.itemInfo.itemId] = true;
                    info.Value.SetBottomUILayoutActive(false);
                }
            }

            PlayerPrefManager.SetString(PlayerPrefManager.PlayerItemOwnedKey,JsonConvert.SerializeObject(isOwnedList));

            if(this.localizationIndex==1)
                this.noticeMessageUIManager.PopUpMessage("구매했쮸! :3",new Vector2(1000,300), new Color(89/255,151/255,60/255,1));
            else
                this.noticeMessageUIManager.PopUpMessage("Purchase completed! :3",new Vector2(1000,400), new Color(89/255,151/255,60/255,1));

        }else{
            if(this.localizationIndex==1)
                this.noticeMessageUIManager.PopUpMessage("재화가 부족해쮸! TㅅT",new Vector2(1000,300));
            else
                this.noticeMessageUIManager.PopUpMessage("You are short of cheese! TㅅT",new Vector2(1000,400));
        }

    }

    private bool IsContainLockedItem()
    {
        foreach(KeyValuePair<ItemType,ItemUIInfo> info in this.selectedItemInfo)
        {
            if(info.Value.itemInfo.isLocked)
                return true; 
        }

        return false;
    }
}

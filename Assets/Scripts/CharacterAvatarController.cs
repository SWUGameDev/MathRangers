using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.U2D.Animation;

[Serializable]
public class ItemAvatarBone
{
    public Image itemImage;

    public ItemType itemType;
}



public class CharacterAvatarController : MonoBehaviour
{
    [SerializeField] private Sprite defaultSprite;

    private Dictionary<int,Sprite> itemSpriteDictionary;

    [SerializeField] private List<ItemAvatarBone> avatarPartsList;

    private Dictionary<ItemType,Image> avatarPartsDictionary;

    [SerializeField] private GameObject characterGameObject;

    private readonly string spriteResourceRootPath = "Images/Final/Character/Skin";
    void Start()
    {
        this.InitializeAvatar();

        this.RegisterShopEvent();

    }

    private void OnDestroy() {
        ShopUIManager.OnItemSelected -= WearItem;        
    }

    private void RegisterShopEvent()
    {
        ShopUIManager.OnItemSelected -= WearItem;
        ShopUIManager.OnItemSelected += WearItem;

        this.itemSpriteDictionary = new Dictionary<int,Sprite>();
    }

    private void InitializeAvatar()
    {
        this.InitializeAvatarInfo();

        this.InitializeAvatarSkin();
    }

    private void InitializeAvatarInfo()
    {
        this.avatarPartsDictionary = new Dictionary<ItemType, Image>();
        foreach(ItemAvatarBone itemAvatarBone in this.avatarPartsList)
        {
            this.avatarPartsDictionary[itemAvatarBone.itemType] = itemAvatarBone.itemImage;
        }
    }

    private void InitializeAvatarSkin()
    {
        string userInfoData =  PlayerPrefs.GetString(DiagnosticManager.userInfoData);
        
        if(userInfoData == null)
            return;
        
        UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(userInfoData);

        if(userInfo.teamType == (int)TeamType.None)
            return;

        string teamColor = "";

        if(userInfo.teamType  == (int)TeamType.Minus)
        {
            teamColor = "Blue";
        }else if(userInfo.teamType  == (int)TeamType.Plus)
        {
            teamColor = "Green";
        }else{
            teamColor = "Red";
        }
        this.SetCurrentPlayerSpriteResolver(teamColor);
    }

    private void SetCurrentPlayerSpriteResolver(string teamColor)
    {
        SpriteResolver[] spriteResolvers = this.characterGameObject.transform.GetComponentsInChildren<SpriteResolver>();

        foreach(SpriteResolver spriteResolver in spriteResolvers)
        {
            spriteResolver.SetCategoryAndLabel(spriteResolver.GetCategory(),$"{teamColor}_{spriteResolver.GetCategory()}");
        }
    }

    private void LoadItemUserList()
    {

    }

    private void WearItem(ItemInfo itemInfo,bool isTakeOn)
    {
        if(isTakeOn == false)
        {
            this.avatarPartsDictionary[itemInfo.itemType].sprite = this.defaultSprite;
            return;
        }
        if(this.itemSpriteDictionary.ContainsKey(itemInfo.itemId))
        {
            this.avatarPartsDictionary[itemInfo.itemType].sprite = this.itemSpriteDictionary[itemInfo.itemId];
        }else{
            Sprite itemSprite = Resources.Load<Sprite>(Path.Combine(this.spriteResourceRootPath,itemInfo.itemResourceFileName));
            this.avatarPartsDictionary[itemInfo.itemType].sprite = itemSprite;
            this.itemSpriteDictionary[itemInfo.itemId] = itemSprite;
        }
    }

}

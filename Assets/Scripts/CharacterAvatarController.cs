using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.U2D.Animation;

public class CharacterAvatarController : MonoBehaviour
{
    [SerializeField] private Sprite defaultSprite;

    private Dictionary<int,Sprite> itemSpriteDictionary;

    private Dictionary<ItemType,SpriteRenderer> avatarPartsDictionary;

    [SerializeField] private GameObject characterGameObject;

    [SerializeField] private GameObject backPrefab;

    [SerializeField] private GameObject headPrefab;

    private readonly string characterMainTag = "CharacterMain";

    private readonly string headTag = "Head";

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


    }

    private void InitializeAvatar()
    {
        this.InitializeAvatarSkin();

        this.InitializeAvatarItemPartsRootSprite();
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

    private void InitializeAvatarItemPartsRootSprite()
    {
        this.avatarPartsDictionary = new Dictionary<ItemType, SpriteRenderer>();

        Transform headRoot = GameObject.FindWithTag(this.headTag).transform;;
        GameObject headObject = GameObject.Instantiate(this.headPrefab,headRoot,false);
        this.avatarPartsDictionary[ItemType.HEAD] = headObject.GetComponent<SpriteRenderer>();

        Transform backRoot = GameObject.FindWithTag(this.characterMainTag).transform;;
        GameObject backObject = GameObject.Instantiate(this.backPrefab,backRoot,false);
        this.avatarPartsDictionary[ItemType.BACK] = backObject.GetComponent<SpriteRenderer>();
    }

    private void WearItem(ItemInfo itemInfo,bool isTakeOn)
    {
        if(this.itemSpriteDictionary == null)
            this.itemSpriteDictionary = new Dictionary<int,Sprite>();

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
            Debug.Log(itemSprite);
            Debug.Log(this.avatarPartsDictionary[itemInfo.itemType].sprite);
            this.avatarPartsDictionary[itemInfo.itemType].sprite = itemSprite;
            this.itemSpriteDictionary[itemInfo.itemId] = itemSprite;
        }
    }

}

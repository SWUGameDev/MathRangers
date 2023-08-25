using System;
using UnityEngine.UI;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class ItemAvatarContainer
{
    [SerializeField] private Transform headItemTransform;

    [SerializeField] private Transform backItemTransform;
}



public class CharacterAvatarController : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private ItemAvatarContainer itemContainer;

    [SerializeField] private List<Sprite> avatarSkin;
    void Start()
    {
        this.InitializeAvatar();
    }

    private void InitializeAvatar()
    {
        this.InitializeAvatarSkin();
    }

    private void InitializeAvatarSkin()
    {
        string userInfoData =  PlayerPrefs.GetString(DiagnosticManager.userInfoData);
        
        if(userInfoData == null)
            return;
        
        UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(userInfoData);

        if(userInfo.teamType == (int)TeamType.None)
            return;

        this.characterImage.sprite = this.avatarSkin[(int)userInfo.teamType];
    }

    private void LoadItemUserList()
    {

    }

}

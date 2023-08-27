using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IconImageController : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite[] iconSprites;

    void Start()
    {
        if(PlayerPrefs.HasKey(IconSelectPanel.userIconKey))
            this.iconImage.sprite = this.iconSprites[PlayerPrefs.GetInt(IconSelectPanel.userIconKey)];
            
        IconSelectPanel.OnProfileChanged += this.ChangeIcon;
    }

    private void ChangeIcon(int index)
    {
        this.iconImage.sprite = this.iconSprites[index];

        PlayerPrefs.SetInt(IconSelectPanel.userIconKey,index);

        FirebaseRealtimeDatabaseManager.Instance.UpdateUserIconInfo(index.ToString());
    }
}

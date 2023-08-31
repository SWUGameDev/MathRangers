using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSelectPanel : MonoBehaviour
{
    [SerializeField] private Button[] iconSelectButtonArray;

    public static Action<int> OnProfileChanged;

    [SerializeField] private GameObject character;

    public static readonly string userIconKey = "PlayerPrefs_userIconKey";
    // void Start()
    // {
    //     for(int index = 0;index< this.iconSelectButtonArray.Length; index++)
    //     {
    //         this.iconSelectButtonArray[index].GetComponent<IconSelectButton>().SetIndex(index);

    //         this.iconSelectButtonArray[index].onClick.AddListener(()=>{
    //             this.transform.gameObject.SetActive(false);
    //         });
    //     }
    // }

    public void SetIconPanelUnActive()
    {
        this.transform.gameObject.SetActive(false);

        this.character.SetActive(true);
    }

    public void SetIconSelectPanelActive()
    {
        this.transform.gameObject.SetActive(true);

        this.character.SetActive(false);
    }

}

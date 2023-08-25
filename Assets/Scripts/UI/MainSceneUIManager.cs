using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainSceneUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text nicknameText;
    void Start()
    {
        string userNickname = PlayerPrefs.GetString(NicknameUIManager.NicknamePlayerPrefsKey);
        userNickname = userNickname == null? "Unknown" : userNickname;
        this.nicknameText.text = userNickname;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

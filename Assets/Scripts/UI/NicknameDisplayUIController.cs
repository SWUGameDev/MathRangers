using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NicknameDisplayUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text nicknameText;
    void Start()
    {
        string userNickname = PlayerPrefs.GetString(NicknameUIManager.NicknamePlayerPrefsKey);
        userNickname = userNickname == null? "Unknown" : userNickname;
        this.nicknameText.text = userNickname;
    }

    public void SetNicknameText(string nickname)
    {
        this.nicknameText.text = nickname;
    }
}

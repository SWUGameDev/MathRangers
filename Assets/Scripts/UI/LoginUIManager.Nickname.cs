using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;

public partial class LoginUIManager : MonoBehaviour
{
    [Header("Nickname")]

    [SerializeField] private GameObject nicknameSettingPanel;

    [SerializeField] private TMP_InputField nicknameInputField;

    [SerializeField] TMP_InputField emailField;

    private readonly string nicknamePattern = @"^([a-zA-Z0-9]{4,16}|[\u0080-\uFFFF]{2,8})$";
    
    private string userId;

    private bool isChecked = false;

    public void InitializeNicknameSettingPanel(string userId)
    {
        Debug.Log($"[InitializeNicknameSettingPanel]{userId}");
        this.nicknameSettingPanel.SetActive(true);
        this.userId = userId;
    }

    public void ConfirmNickname()
    {
        if(this.isChecked)
            return;

        string nickName = nicknameInputField.text;
        if(!IsNicknameValid(nickName))
        {
            LoginUIManager.Instance.PopUpMessage("! 맞지 않는 닉네임 형식입니다.");
            return;    
        }

        {
            UserInfo userInfo = new UserInfo(this.emailField.text,nickName);
            string serializedData = JsonUtility.ToJson(userInfo);
            FirebaseRealtimeDatabaseManager.Instance.UploadUserInfo(this.userId,serializedData,this.ChangeScene);
            this.isChecked = true;
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("03_MainScene");
    }

    private bool IsNicknameValid(string nickname)
    {
        return Regex.Match(nickname, nicknamePattern).Success;
    }
}

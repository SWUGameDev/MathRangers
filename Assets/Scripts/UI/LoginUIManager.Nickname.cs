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

    private readonly string nicknamePattern = @"^[\w가-힣]{1,8}$";
    
    private string userId;

    private bool isChecked = false;

    public void InitializeNicknameSettingPanel(string userId)
    {
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

        FirebaseRealtimeDatabaseManager.Instance.CheckDuplicateNickname(nickName,OnNicknameCheckFailed,OnNicknameCheckDuplicated,OnNicknameCheckCompleted);
    }

    private void OnNicknameCheckFailed()
    {
        LoginUIManager.Instance.PopUpMessage("! [Error] Can't Access to Firebase Service");
        this.isChecked = true;
    }

    private void OnNicknameCheckCompleted(string nickName)
    {
        UserInfo userInfo = new UserInfo(FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserEmail(),nickName);
        string serializedData = JsonUtility.ToJson(userInfo);
        FirebaseRealtimeDatabaseManager.Instance.UploadInitializedUserInfo(this.userId,serializedData,this.LoadDiagnosticScene);
        this.isChecked = true;
    }

    private void OnNicknameCheckDuplicated(string nickName)
    {
        LoginUIManager.Instance.PopUpMessage($" ! {nickName}은 이미 존재하는 닉네임 입니다. ");
        this.isChecked = true;
    }

    private void LoadDiagnosticScene()
    {
        PlayerPrefs.SetInt("NicknameSettingCompleted",1);
        
        SceneManager.LoadScene("04_DiagnosticScene");
    }

    private bool IsNicknameValid(string nickname)
    {
        return Regex.Match(nickname, nicknamePattern).Success;
    }
}

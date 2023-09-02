using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;

public partial class NicknameUIManager : MonoBehaviour
{
    [Header("Nickname")]

    [SerializeField] private NoticeMessageUIManager noticeMessageUIManager;

    [SerializeField] private TMP_InputField nicknameInputField;

    private readonly string nicknamePattern = @"^[\w가-힣]{1,8}$";
    
    private string userId;

    private string nickname;

    private bool isChecked = false;

    private static readonly string NickNameSettingSceneName = "03_NicknameSettingScene";

    public static readonly string NicknamePlayerPrefsKey = "NicknamePlayerPrefsKey";

    public static Action<bool> OnNicknameConfirmed;

    private int languageIndex;

    private void Start()
    {
        this.languageIndex = LocalizationManager.Instance.GetCurrentLocalizationIndex();
    }

    public void ConfirmNickname()
    {
        if(this.nickname == nicknameInputField.text && this.isChecked)
            return;

        this.nickname = nicknameInputField.text;

        if(!IsNicknameValid(this.nickname))
        {
            if(this.languageIndex==1)
                this.noticeMessageUIManager.PopUpMessage("! 맞지 않는 닉네임 형식입니다.");
            else
                this.noticeMessageUIManager.PopUpMessage("! Incorrect nickname format.");
            return;    
        }

        FirebaseRealtimeDatabaseManager.Instance.CheckDuplicateNickname(this.nickname,OnNicknameCheckFailed,OnNicknameCheckDuplicated,OnNicknameCheckCompleted);
    }

    private void OnNicknameCheckFailed()
    {
        if(this.languageIndex==1)
            this.noticeMessageUIManager.PopUpMessage("! [Error] 현재 서버 데이터에 접근이 불가능합니다.");
        else
            this.noticeMessageUIManager.PopUpMessage("! [Error] Can't Access to Firebase Service");

        NicknameUIManager.OnNicknameConfirmed?.Invoke(false);

        this.isChecked = true;
    }

    private void OnNicknameCheckCompleted(string nickName)
    {
        // TODO : 조건 확인 로직 현재 씬 이름 여부 말고 다른 방식으로 변경하기
        
        if(SceneManager.GetActiveScene().name == NicknameUIManager.NickNameSettingSceneName)
        {
            UserInfo userInfo = new UserInfo(FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserEmail(),"",nickName,-1);

            PlayerPrefs.SetString(NicknameUIManager.NicknamePlayerPrefsKey,nickName);

            string serializedData = JsonUtility.ToJson(userInfo);

            FirebaseRealtimeDatabaseManager.Instance.UploadInitializedUserInfo(this.userId,serializedData,this.LoadDiagnosticScene);
        }

        NicknameUIManager.OnNicknameConfirmed?.Invoke(true);

        this.isChecked = true;
    }

    private void OnNicknameCheckDuplicated(string nickName)
    {
        if(this.languageIndex==1)
            this.noticeMessageUIManager.PopUpMessage($" ! {nickName}은 이미 존재하는 닉네임 입니다. ");
        else
            this.noticeMessageUIManager.PopUpMessage($" ! {nickName} already exists. ");

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

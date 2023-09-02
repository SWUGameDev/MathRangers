using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public partial class AuthManager : MonoBehaviour
{ 

    [SerializeField] NoticeMessageUIManager noticeMessageUIManager;
    [SerializeField] TMP_InputField emailField;
    [SerializeField] TMP_InputField passwordField;
    Firebase.Auth.FirebaseAuth auth;
    private int languageIndex = 0;

    void Awake()
    { 
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        this.languageIndex = LocalizationManager.Instance.GetCurrentLocalizationIndex();
    }
    public void login()
    {
        if(!this.IsEmailValid(emailField.text))
        {
            if(this.languageIndex)
                this.noticeMessageUIManager.PopUpMessage("! 맞지 않는 이메일 형식입니다.");
            else
                this.noticeMessageUIManager.PopUpMessage("! Incorrect nickname format.");
            return;
        }

        if(!this.IsPasswordValid(passwordField.text))
        {
            if(this.languageIndex)
                this.noticeMessageUIManager.PopUpMessage("비밀번호 형식이 잘못되었습니다. \n영문자와 숫자가 포함되어야하며 5자리 이상이여야 합니다.");
            else
                this.noticeMessageUIManager.PopUpMessage("! Incorrect password format.<br>It must be at least 6 characters including at least one letter and one number.");
            return;
        }


        auth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(
            task =>
            {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Firebase.Auth.AuthResult result = task.Result;

                    Debug.Log(emailField.text + "로 로그인되었습니다. \n"); 

                    this.OnSignInCompleted();   
                    
                }
                else if (task.IsFaulted)
                {
                    this.LoginErrorHandler(task.Exception.Flatten().InnerExceptions);
                }
            },TaskScheduler.FromCurrentSynchronizationContext()
        );
    }

    private void OnSignInCompleted()
    {
        if(!PlayerPrefs.HasKey("NicknameSettingCompleted"))
        {
            SceneManager.LoadScene("03_NicknameSettingScene");   
        }
        else if(!PlayerPrefs.HasKey("DiagnosticCompleted"))
        {
            SceneManager.LoadScene("04_DiagnosticScene");  
        }
        else
        {
            SceneManager.LoadScene("03_MainScene");   
        }
    }
    public void register() {

        if(!this.IsEmailValid(emailField.text))
        {
            if(this.languageIndex)
                this.noticeMessageUIManager.PopUpMessage("이메일 형식이 잘못되었습니다.");
            else
                this.noticeMessageUIManager.PopUpMessage("! Incorrect email format.");
            return;
        }

        if(!this.IsPasswordValid(passwordField.text))
        {
            if(this.languageIndex)
                this.noticeMessageUIManager.PopUpMessage("비밀번호 형식이 잘못되었습니다. \n영문자와 숫자가 포함되어야하며 5자리 이상이여야 합니다.");
            else
                this.noticeMessageUIManager.PopUpMessage("! Incorrect password format.<br>It must be at least 6 characters including at least one letter and one number.");
            return;
        }


        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(
            task => {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Firebase.Auth.AuthResult result = task.Result;  

                    Debug.Log(emailField.text + $"로 회원가입되었습니다.");

                    SceneManager.LoadScene("03_NicknameSettingScene");
                }
                else if (task.IsFaulted)
                {
                    this.SignUpErrorHandler(task.Exception.Flatten().InnerExceptions);

                }else{
                    Debug.Log("회원가입 실패");
                }
            },TaskScheduler.FromCurrentSynchronizationContext()
        );
    }

}
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

    [SerializeField] TMP_InputField emailField;
    [SerializeField] TMP_InputField passwordField;
    Firebase.Auth.FirebaseAuth auth;

    void Awake()
    { 
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    public void login()
    {
        if(!this.IsEmailValid(emailField.text))
        {
            LoginUIManager.Instance.PopUpMessage("! 맞지 않는 이메일 형식입니다.");
            return;
        }

        if(!this.IsPasswordValid(passwordField.text))
        {
            LoginUIManager.Instance.PopUpMessage("비밀번호 형식이 잘못되었습니다. \n영문자와 숫자가 포함되어야하며 5자리 이상이여야 합니다.");
            return;
        }


        auth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(
            task =>
            {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Firebase.Auth.AuthResult result = task.Result;
                    Debug.Log(emailField.text + "로 로그인되었습니다. \n");    

                    FirebaseRealtimeDatabaseManager.Instance.LoadUserInfo(result.User.UserId, this.OnSignInCompleted);

                    
                }
                else if (task.IsFaulted)
                {
                    this.LoginErrorHandler(task.Exception.Flatten().InnerExceptions);
                }
            },TaskScheduler.FromCurrentSynchronizationContext()
        );
    }

    private void OnSignInCompleted(UserInfo userInfo)
    {
        if(userInfo == null)
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
            LoginUIManager.Instance.PopUpMessage("이메일 형식이 잘못되었습니다.");
            return;
        }

        if(!this.IsPasswordValid(passwordField.text))
        {
            LoginUIManager.Instance.PopUpMessage("비밀번호 형식이 잘못되었습니다. \n영문자와 숫자가 포함되어야하며 5자리 이상이여야 합니다.");
            return;
        }


        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(
            task => {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Firebase.Auth.AuthResult result = task.Result;  

                    Debug.Log(emailField.text + $"로 회원가입되었습니다.");

                    SceneManager.LoadScene("01_TitleScene");
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
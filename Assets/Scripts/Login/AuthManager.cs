using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

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
                }
                else if (task.IsFaulted)
                {
                    // 비밀번호가 틀린 경우 처리
                    foreach (var innerException in task.Exception.Flatten().InnerExceptions)
                    {
                        if (innerException is FirebaseException firebaseException)
                        {
                            if ((AuthError)firebaseException.ErrorCode == AuthError.WrongPassword)
                            {
                                LoginUIManager.Instance.PopUpMessage("! 비밀번호를 다시 확인해주세요. ");
                            }else if((AuthError)firebaseException.ErrorCode == AuthError.WrongPassword){
                                LoginUIManager.Instance.PopUpMessage("! 존재하지 않는 이메일입니다.\n회원가입을 진행해주세요.");
                            }
                            else
                            {
                                Debug.LogWarning($"{(AuthError)firebaseException.ErrorCode} SignInWithEmailAndPasswordAsync encountered an error: {firebaseException.Message}");
                            }
                        }
                    }
                }
            },TaskScheduler.FromCurrentSynchronizationContext()
        );
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
                    LoginUIManager.Instance.InitializeNicknameSettingPanel(result.User.UserId);
                }
                else if (task.IsFaulted)
                {
                    foreach (var innerException in task.Exception.Flatten().InnerExceptions)
                    {
                        if (innerException is FirebaseException firebaseException)
                        {
                            if ((AuthError)firebaseException.ErrorCode == AuthError.EmailAlreadyInUse)
                            {
                                LoginUIManager.Instance.PopUpMessage("! 이미 회원가입 된 이메일 입니다.");
                            }
                            else
                            {
                                Debug.LogWarning($"{(AuthError)firebaseException.ErrorCode} CreateUserWithEmailAndPasswordAsync encountered an error: " + firebaseException.Message);
                            }
                        }
                    }

                }else{
                    Debug.Log("회원가입 실패");
                }
            },TaskScheduler.FromCurrentSynchronizationContext()
        );
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using TMPro;

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
            task => {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Firebase.Auth.AuthResult result = task.Result;
                    Debug.Log($"{emailField.text} 로 로그인 하셨습니다. {result.User.UserId}");
                }
                else
                {
                    LoginUIManager.Instance.PopUpMessage($"로그인이 실패하였습니다.\n{task.Exception}");
                }
            }
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
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    Debug.Log(emailField.text + "로 회원가입되었습니다. \n");
                }
                else
                    Debug.Log("회원가입에 실패하였습니다. \n");
            }
        );
    }

}
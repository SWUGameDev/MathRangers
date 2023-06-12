using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public partial class AuthManager: MonoBehaviour {

    private void LoginErrorHandler(InnerExceptions innerExceptions) {
        foreach(var innerException in innerExceptions) {

            if (innerException is FirebaseException firebaseException) {
                if ((AuthError)firebaseException.ErrorCode == AuthError.WrongPassword) {
                    LoginUIManager
                        .Instance
                        .PopUpMessage("! 비밀번호를 다시 확인해주세요. ");
                } else if ((AuthError)firebaseException.ErrorCode == AuthError.WrongPassword) {
                    LoginUIManager
                        .Instance
                        .PopUpMessage("! 존재하지 않는 이메일입니다.\n회원가입을 진행해주세요.");
                } else {
                    Debug.LogWarning(
                        $"{(AuthError)firebaseException.ErrorCode} SignInWithEmailAndPasswordAsync encou" +
                        "ntered an error: {firebaseException.Message}"
                    );
                }
            }
        }
    }

    private void SignUpErrorHandler(InnerExceptions innerExceptions) {
        foreach(var innerException in innerExceptions) {
            if (innerException is FirebaseException firebaseException) {
                if ((AuthError)firebaseException.ErrorCode == AuthError.EmailAlreadyInUse) {
                    LoginUIManager
                        .Instance
                        .PopUpMessage("! 이미 회원가입 된 이메일 입니다.");
                } else {
                    Debug.LogWarning(
                        $"{(AuthError)firebaseException.ErrorCode} CreateUserWithEmailAndPasswordAsync e" +
                        "ncountered an error: " + firebaseException.Message
                    );
                }
            }
        }
    }

}
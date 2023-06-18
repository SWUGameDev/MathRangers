using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public partial class AuthManager: MonoBehaviour {

    private void LoginErrorHandler(System.Collections.ObjectModel.ReadOnlyCollection<System.Exception>  innerExceptions) {
        foreach(var innerException in innerExceptions) {
                if (!(innerException is FirebaseException firebaseException))
                    continue;

                switch((AuthError) firebaseException.ErrorCode )
                {
                    case AuthError.WrongPassword : 
                        LoginUIManager.Instance.PopUpMessage("! 비밀번호를 다시 확인해주세요. ");
                        break;
                    case AuthError.InvalidEmail :
                        LoginUIManager.Instance.PopUpMessage("! 맞지 않는 아이디 형식입니다.");
                        break;
                    case AuthError.UserNotFound :
                        LoginUIManager.Instance.PopUpMessage("! 찾을 수 없는 아이디입니다.\n회원가입을 진행해주세요.");
                        break;
                    default: 
                        LoginUIManager.Instance.PopUpMessage($"! Error Error code {((AuthError)firebaseException.ErrorCode).ToString()}");
                        break;        
                }    
            }
        }

    private void SignUpErrorHandler(System.Collections.ObjectModel.ReadOnlyCollection<System.Exception>  innerExceptions) {
        foreach(var innerException in innerExceptions) {
                if (!(innerException is FirebaseException firebaseException))
                    continue;

                switch((AuthError) firebaseException.ErrorCode )
                {
                    case AuthError.EmailAlreadyInUse : 
                        LoginUIManager.Instance.PopUpMessage("! 이미 회원가입 된 이메일 입니다.");
                        break;
                    default: 
                        LoginUIManager.Instance.PopUpMessage($"! Error Error code {firebaseException.ErrorCode}");
                        break;    
                }    
            }
    }
}

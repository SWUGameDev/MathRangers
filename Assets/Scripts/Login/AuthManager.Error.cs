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
                        if(this.languageIndex==1)
                            this.noticeMessageUIManager.PopUpMessage("! 비밀번호를 다시 확인해주세요. ");
                        else
                            this.noticeMessageUIManager.PopUpMessage("! Please check your password again.");
                        break;
                    case AuthError.InvalidEmail :
                        if(this.languageIndex==1)
                            this.noticeMessageUIManager.PopUpMessage("! 맞지 않는 아이디 형식입니다.");
                        else
                            this.noticeMessageUIManager.PopUpMessage("! The ID format is incorrect.");
                        break;
                    case AuthError.UserNotFound :
                        if(this.languageIndex==1)
                            this.noticeMessageUIManager.PopUpMessage("! 찾을 수 없는 아이디입니다.\n회원가입을 진행해주세요.");
                        else
                            this.noticeMessageUIManager.PopUpMessage("! This ID cannot be found.\nPlease proceed with membership registration.");
                        break;
                    default: 
                            this.noticeMessageUIManager.PopUpMessage($"! [Error] Error code {((AuthError)firebaseException.ErrorCode).ToString()}");
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
                        if(this.languageIndex==1)
                            this.noticeMessageUIManager.PopUpMessage("! 이미 회원가입 된 이메일 입니다.");
                        else
                            this.noticeMessageUIManager.PopUpMessage("! This email address has already been registered as a member.");
                        break;
                    case AuthError.WeakPassword:
                        if(this.languageIndex==1)
                            this.noticeMessageUIManager.PopUpMessage("! 비밀번호는 6글자 이상이여야 합니다.");
                        else
                            this.noticeMessageUIManager.PopUpMessage("! The password must be at least 6 characters.");
                        break;
                    default:
                            this.noticeMessageUIManager.PopUpMessage($"! [Error] Error code {firebaseException.ErrorCode.ToString() }");
                        break;    
                }    
            }
    }
}

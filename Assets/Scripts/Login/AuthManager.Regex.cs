using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public partial class AuthManager : MonoBehaviour
{ 
    private readonly string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

    private readonly string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d).{5,}$";
    
    private bool IsEmailValid(string email)
    {
        return Regex.Match(email, emailPattern).Success;
    }

    private bool IsPasswordValid(string password)
    {
        return Regex.Match(password, passwordPattern).Success;
    }
}
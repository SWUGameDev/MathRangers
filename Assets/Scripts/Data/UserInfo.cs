using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo
{
    [SerializeField]
    string _email;

    [SerializeField]
    string _nickname;

    public UserInfo()
    {

    }
    
    public UserInfo(string email,string nickname)
    {
        this._email = email;
        this._nickname = nickname;
    }
}

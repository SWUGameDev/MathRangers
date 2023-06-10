using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo
{
    [SerializeField]
    public string email;

    [SerializeField]
    public string nickname;

    public UserInfo()
    {

    }
    
    public UserInfo(string email,string nickname)
    {
        this.email = email;
        this.nickname = nickname;
    }
}

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

    [SerializeField]
    public int teamType;

    public UserInfo()
    {

    }
    
    public UserInfo(string email,string nickname,int teamType)
    {
        this.email = email;
        this.nickname = nickname;
        this.teamType = teamType;
    }
}

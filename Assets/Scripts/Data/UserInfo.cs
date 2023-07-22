using System;
using UnityEngine;

[Serializable]
public class UserInfo
{
    [SerializeField]
    public string email;

    [SerializeField]
    public string nickname;

    [SerializeField]
    public string userAuthorization;

    [SerializeField]
    public int teamType;

    public UserInfo()
    {

    }
    
    public UserInfo(string email,string nickname,string userAuthorization,int teamType)
    {
        this.email = email;
        this.nickname = nickname;
        this.userAuthorization = userAuthorization;
        this.teamType = teamType;
    }
}

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
    public string userMRBId;

    [SerializeField]
    public int teamType;

    public UserInfo()
    {

    }
    
    public UserInfo(string email,string nickname,string userMRBId,int teamType)
    {
        this.email = email;
        this.nickname = nickname;
        this.userMRBId = userMRBId;
        this.teamType = teamType;
    }
}

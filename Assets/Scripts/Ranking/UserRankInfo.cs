[System.Serializable]
public class UserRankInfo
{
    public string UID;
    public int iconId;
    public string nickname;

    public long score;

    public int team;

    public UserRankInfo()
    {

    }

    public UserRankInfo(string uid,int iconId,string nickname,long score,int teamType)
    {
        this.UID = uid;
        this.iconId = iconId;
        this.nickname = nickname;
        this.score = score;
        this.team = teamType;
    }
}
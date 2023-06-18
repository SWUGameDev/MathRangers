using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using TeamName = System.String;
using totalScore = System.Int64;
using System.Linq;

public partial class RankingManager : MonoBehaviour
{
    private List<UserRankInfo> OrderByUserScore()
    {
        if(this.infos == null)
        {
            this.LoadRankingData();
            return null;
        }
        this.infos.Sort((x, y) => y.score.CompareTo(x.score));
        return this.infos;
    }

}

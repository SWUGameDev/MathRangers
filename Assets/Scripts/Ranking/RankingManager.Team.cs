using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using TeamName = System.String;
using totalScore = System.Int64;
using System.Linq;

public partial class RankingManager : MonoBehaviour
{

    private IOrderedEnumerable<KeyValuePair<TeamName, totalScore>> CalculateTotalSumByTeams()
    {
        if(this.infos==null)
        {
            this.LoadRankingData();
            return null;
        }
        Dictionary<TeamName, totalScore> nameScoreSum = new Dictionary<TeamName, totalScore>();
        foreach (var  user in infos)
        {
            if (nameScoreSum.ContainsKey(user.team))
            {
                nameScoreSum[user.team] += user.score;
            }
            else
            {
                nameScoreSum[user.team] = user.score;
            }
        }
        var sortedNameScoreSum = nameScoreSum.OrderByDescending(pair => pair.Value);
    
        return sortedNameScoreSum;
    }

}

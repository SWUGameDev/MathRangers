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
        if(this.infos == null)
        {
            this.LoadRankingData();
            return null;
        }

        Dictionary<TeamName, totalScore> TotalScoreByTeamDictionary = new Dictionary<TeamName, totalScore>();
        foreach (var user in infos)
        {
            if (TotalScoreByTeamDictionary.ContainsKey(user.team))
            {
                TotalScoreByTeamDictionary[user.team] += user.score;
            }
            else
            {
                TotalScoreByTeamDictionary[user.team] = user.score;
            }
        }
        var sortedScoreByTeams = TotalScoreByTeamDictionary.OrderByDescending(pair => pair.Value);
    
        return sortedScoreByTeams;
    }

}

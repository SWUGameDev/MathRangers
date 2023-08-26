using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using totalScore = System.Int64;
using System.Linq;

public partial class RankingManager : MonoBehaviour
{

    private IOrderedEnumerable<KeyValuePair<TeamType, totalScore>> CalculateTotalSumByTeams()
    {
        if(this.infos == null)
        {
            this.LoadRankingData();
            return null;
        }

        Dictionary<TeamType, totalScore> TotalScoreByTeamDictionary = new Dictionary<TeamType, totalScore>();
        foreach (var user in infos)
        {
            TeamType teamType = (TeamType)user.team;
            if (TotalScoreByTeamDictionary.ContainsKey(teamType))
            {
                TotalScoreByTeamDictionary[teamType] += user.score;
            }
            else
            {
                TotalScoreByTeamDictionary[teamType] = user.score;
            }
        }
        var sortedScoreByTeams = TotalScoreByTeamDictionary.OrderByDescending(pair => pair.Value);
    
        return sortedScoreByTeams;
    }

}

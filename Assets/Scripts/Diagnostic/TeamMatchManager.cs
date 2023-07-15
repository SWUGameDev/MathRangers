using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TeamType:int
{
    Plus = 0,
    Minus = 1,
    Multiple = 2
}

public class TeamMatchManager : MonoBehaviour
{
    private static TeamMatchManager instance;

    public int[] score = new int[3];

    public static TeamMatchManager GetInstance()
    {
        return TeamMatchManager.instance;
    }

    public void SetTeamMatchScore(TeamType type,int score)
    {
        this.score[(int)type] += score;
    }

    private List<int> FindMaxIndices(List<int> data)
    {
        if (data.Count == 0)
            return new List<int>();

        int max_value = data[0];
        List<int> max_indices = new List<int> { 0 };
        for (int i = 1; i < data.Count; i++)
        {
            if (data[i] > max_value)
            {
                max_value = data[i];
                max_indices = new List<int> { i };
            }
            else if (data[i] == max_value)
            {
                max_indices.Add(i);
            }
        }

        return max_indices;
    }

    public int GetSelectedTeam()
    {
        List<int> maxScores = this.FindMaxIndices(this.score.ToList());

        if(maxScores.Count==1)
        {
            return maxScores[0];
        }else{
            switch (maxScores[0])
            {
                case 0:
                    return maxScores[1] == 1 ? 0 : 2;
                case 1:
                    return maxScores[1] == 2 ? 1 : 0;
                case 2:
                    return maxScores[1] == 0 ? 2 : 1;
                default:
                    return 0;
            }
        }
    }

    public void PrintCurrentMatchScore()
    {
        for(int index= 0;index<this.score.Length;index++)
        {
            Debug.Log($"{((TeamType)index).ToString()}  score : {score[index]}");
        }
    }

    private void Awake()
    {
        TeamMatchManager.instance = this;
    }
}

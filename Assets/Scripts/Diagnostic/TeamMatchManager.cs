using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TeamType:int
{
    // 초록색
    Plus = 0,
    // 파란색
    Minus = 1,
    // 빨강색
    Multiple = 2,
    None = -1
}

public class TeamMatchManager : MonoBehaviour
{
    private static TeamMatchManager instance;

    [SerializeField] private int[] score = new int[3];

    private TeamType teamType = TeamType.None;

    private void Awake()
    {
        TeamMatchManager.instance = this;
    }

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

    private void SetTeamType()
    {
        List<int> maxScores = this.FindMaxIndices(this.score.ToList());

        if(maxScores.Count==1)
        {
            this.teamType =  (TeamType)maxScores[0];
        }else{
            switch (maxScores[0])
            {
                case 0:
                    this.teamType = maxScores[1] == (int)TeamType.Minus ? TeamType.Plus : TeamType.Multiple;
                    break;
                case 1:
                    this.teamType = maxScores[1] == (int)TeamType.Multiple ? TeamType.Minus : TeamType.Plus;
                    break;
                case 2:
                    this.teamType = maxScores[1] == (int)TeamType.Plus ? TeamType.Multiple : TeamType.Minus;
                    break;
                default:
                    this.teamType = TeamType.None;
                    break;
            }
        }
    }

    public int GetSelectedTeam()
    {
        if(this.teamType == TeamType.None)
        {
            this.SetTeamType();
        }

        return (int)this.teamType;
    }

    public void PrintCurrentMatchScore()
    {
        for(int index= 0;index<this.score.Length;index++)
        {
            Debug.Log($"{((TeamType)index).ToString()}  score : {score[index]}");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class RankingUIItem : MonoBehaviour
{
    [SerializeField] private Image itemBackgroundImage;
    
    [SerializeField] private Image rankBackgroundImage;

    [SerializeField] private TMP_Text rankText;

    [SerializeField] private Image userIconImage;

    [SerializeField] private TMP_Text userName;

    [SerializeField] private TMP_Text userScore;

    public void InitializeRankingItemController(int rank,UserRankInfo userRankInfo)
    {
        this.rankText.text = rank.ToString();
        this.userName.text = userRankInfo.nickname;
        string formattedNumber =  userRankInfo.score.ToString("N0");
        this.userScore.text = formattedNumber;
    }

    public void SetItemBackGroundImage(Color color)
    {
        this.itemBackgroundImage.color = color;
    }

    public void SetRankBackGroundImage(Color color)
    {
        this.rankBackgroundImage.color = color;
    }

}

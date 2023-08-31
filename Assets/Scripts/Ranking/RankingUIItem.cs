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

    [SerializeField] private GameObject myRankingImage;

    [SerializeField] private TMP_Text userName;

    [SerializeField] private TMP_Text userScore;

    public void InitializeRankingItemController(int rank,UserRankInfo userRankInfo,Sprite sprite)
    {
        this.rankText.text = rank.ToString();
        this.userName.text = userRankInfo.nickname;
        string formattedNumber =  userRankInfo.score.ToString("N0");
        this.userScore.text = formattedNumber;
        this.userIconImage.sprite = sprite;
    }

    public void SetItemBackGroundImage(Sprite sprite)
    {
        this.itemBackgroundImage.sprite = sprite;
    }

    public void SetRankBackGroundImage(Sprite sprite)
    {
        this.rankBackgroundImage.sprite = sprite;
    }

    public void SetMyRankBackGroundActive(bool isActive )
    {
        this.myRankingImage.SetActive(isActive);
    }

}

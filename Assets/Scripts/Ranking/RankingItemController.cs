using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingItemController : MonoBehaviour
{
    [SerializeField] private Image itemBackgroundImage;
    
    [SerializeField] private Image rankBackgroundImage;

    [SerializeField] private TMP_Text rankTextt;

    [SerializeField] private Image userIconImage;

    [SerializeField] private TMP_Text userName;

    [SerializeField] private TMP_Text userScore;

    public RankingItemController()
    {

    }

}

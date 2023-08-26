using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TeamRackingUIItem : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;

    [SerializeField] private TeamType teamType;

    public TeamType TeamType{
        get {return this.teamType;}
    }

    [SerializeField] private TMP_Text teamRankText;

    [SerializeField] private Image teamRankTextBackgroundImage;

    public void SetTeamRankBackGroundColor(Color color)
    {
        this.teamRankTextBackgroundImage.color = color;
    }

    public void SetTeamRankText(string text)
    {
        this.teamRankText.text = text;
    }
    
}

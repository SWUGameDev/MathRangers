using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RankingGroupButton : MonoBehaviour, IGroupButton
{
    [SerializeField] private Image buttonImage;

    private ColorOption colorOption;

    private RankingUIManager rankingUIManager;

    private int index;

    public void InitializeRankingGroupButton(int index,ColorOption colorOption,RankingUIManager rankingUIManager)
    {
        this.index = index;
        this.colorOption = colorOption;
        this.rankingUIManager = rankingUIManager;
    }

    public void OnActivatedButtonChanged(int activatedIndex)
    {
        if(this.index != activatedIndex)
            this.buttonImage.color = this.colorOption.unActivatedColor;
        else
        {
            this.buttonImage.color = this.colorOption.activatedColor;
        }
    }

    public void ButtonClicked()
    {
        this.rankingUIManager.OnActivatedButtonChanged.Invoke(this.index);
    }
}

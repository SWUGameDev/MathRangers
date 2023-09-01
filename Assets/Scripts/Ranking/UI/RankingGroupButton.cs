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

    private GameObject currentTargetPanel;

    private int index;

    public void InitializeRankingGroupButton(int index,GameObject currentTargetPanel,ColorOption colorOption,RankingUIManager rankingUIManager)
    {
        this.index = index;
        this.currentTargetPanel = currentTargetPanel;
        this.colorOption = colorOption;
        this.rankingUIManager = rankingUIManager;
    }

    public void OnActivatedButtonChanged(int activatedIndex)
    {
        if(this.index != activatedIndex)
        {
            this.buttonImage.sprite = this.colorOption.unActivatedSprite;
            if(this.currentTargetPanel.activeSelf == true)
                this.currentTargetPanel?.SetActive(false);
        }
        else
        {
            this.buttonImage.sprite = this.colorOption.activatedSprite;
            this.currentTargetPanel?.SetActive(true);
        }
    }

    public void ButtonClicked()
    {
        this.rankingUIManager.OnActivatedButtonChanged.Invoke(this.index);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

[Serializable]
public class ColorOption
{
    [SerializeField] public Color activatedColor;

    [SerializeField] public Color unActivatedColor;
}

[Serializable]
public class RankPanelUI
{
    [SerializeField] public Button rankListButton;

    [SerializeField] public GameObject rankListPanel;
}

public partial class RankingUIManager : MonoBehaviour
{
    [SerializeField] private List<RankPanelUI> rankPanelUIs;

    [SerializeField] public ColorOption colorOption;

    public UnityEvent<int> OnActivatedButtonChanged;

    private void Awake() {
        this.InitializeButtonSettings();

        //TODO: 각 그룹 별 초기화 타이밍에 시작하도록 시점 변경
        this.initializeTeamRankSettings();
    }

    private void InitializeButtonSettings()
    {
        this.OnActivatedButtonChanged = new UnityEvent<int>();
        for(int index = 0;index<this.rankPanelUIs.Count; index++)
        {
            RankingGroupButton rankListButton = this.rankPanelUIs[index].rankListButton.gameObject.GetComponent<RankingGroupButton>();
            rankListButton.InitializeRankingGroupButton(index,this.rankPanelUIs[index].rankListPanel,this.colorOption,this);
            this.OnActivatedButtonChanged.AddListener(rankListButton.OnActivatedButtonChanged);
        }
    }


    private void OnDestroy() {
        this.OnActivatedButtonChanged?.RemoveAllListeners();
    }
    
}

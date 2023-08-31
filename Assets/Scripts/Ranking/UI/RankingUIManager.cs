using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TabName = System.String;

[Serializable]
public class ColorOption
{
    [SerializeField] public Sprite activatedSprite;

    [SerializeField] public Sprite unActivatedSprite;
}

[Serializable]
public class RankPanelUI
{
    [SerializeField] public Button rankListButton;

    [SerializeField] public GameObject rankListPanel;
}

public partial class RankingUIManager : MonoBehaviour
{
    [Header("RankingScene Main UI")]

    [SerializeField] private List<RankPanelUI> rankPanels;

    [SerializeField] public ColorOption colorOption;

    public UnityEvent<int> OnActivatedButtonChanged;
    private void Awake() {
        this.InitializeButtonSettings();
    }

    private void Start()
    {
        SoundManager.Instance.ChangeBackgroundAudioSource(backgroundAudioSourceType.BGM_RANKING);
    }

    private void InitializeButtonSettings()
    {

        this.OnActivatedButtonChanged = new UnityEvent<int>();
        for(int index = 0;index<this.rankPanels.Count;index++)
        {
            RankingGroupButton rankListButton = this.rankPanels[index].rankListButton.gameObject.GetComponent<RankingGroupButton>();
            rankListButton.InitializeRankingGroupButton(index,this.rankPanels[index].rankListPanel,this.colorOption,this);
            this.OnActivatedButtonChanged.AddListener(rankListButton.OnActivatedButtonChanged);
        }
    }

    private void OnDestroy() {
        this.OnActivatedButtonChanged?.RemoveAllListeners();
    }
    
}

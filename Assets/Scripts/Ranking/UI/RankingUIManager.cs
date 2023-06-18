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

public class RankingUIManager : MonoBehaviour
{
    [SerializeField] private Button[] rankListButtons;

    [SerializeField] public ColorOption colorOption;

    public UnityEvent<int> OnActivatedButtonChanged;

    private void Awake() {
        this.InitializeButtonSettings();
    }

    private void InitializeButtonSettings()
    {
        this.OnActivatedButtonChanged = new UnityEvent<int>();
        for(int index = 0;index<this.rankListButtons.Length; index++)
        {
            RankingGroupButton rankListButton = this.rankListButtons[index].gameObject.GetComponent<RankingGroupButton>();
            rankListButton.InitializeRankingGroupButton(index,this.colorOption,this);
            this.OnActivatedButtonChanged.AddListener(rankListButton.OnActivatedButtonChanged);
        }
    }

    private void OnDestroy() {
        this.OnActivatedButtonChanged?.RemoveAllListeners();
    }
    
}

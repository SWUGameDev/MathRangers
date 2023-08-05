using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityInfoUIController : MonoBehaviour {
    
    [SerializeField]
    private Image abilityImage;

    [SerializeField]
    private TMP_Text AbilityNameText;

    [SerializeField]
    private TMP_Text AbilityDescriptionText;

    [SerializeField]
    private Button abilitySelectedButton;

    private AbilityInfoManager abilityInfoManager;

    private AbilityInfo abilityInfo;

    private void Start()
    {
        this.abilitySelectedButton.onClick.AddListener(() =>
        {
            this.abilityInfoManager.SelectAbility(this.abilityInfo.abilityId);
        });
    }


    public void InitializeAbilityUIInfo(AbilityInfoManager abilityInfoManager,AbilityInfo abilityInfo)
    {
        this.abilityInfo = abilityInfo;

        this.abilityInfoManager = abilityInfoManager;

        this.abilityImage.sprite = abilityInfo.abilityIcon;

        var dictionary =  abilityInfoManager.GetSelectedAbilityDictionary();

        int level = 0;

        if(dictionary.ContainsKey(abilityInfo.abilityId))
        {
            level = dictionary[abilityInfo.abilityId].selectedCount;
        }

        if (LocalizationManager.Instance.GetCurrentLocalizationIndex() == 1)
        {
            this.AbilityNameText.text = abilityInfo.abilityName.koreanName;
            this.AbilityDescriptionText.text = this.abilityDescriptionFormatting(abilityInfo,abilityInfo.abilityDescription.koreanDescription,level);
        }
        else {
            this.AbilityNameText.text = abilityInfo.abilityName.englishName;
            this.AbilityDescriptionText.text = this.abilityDescriptionFormatting(abilityInfo,abilityInfo.abilityDescription.englishDescription,level);
        }

    }

    private string abilityDescriptionFormatting(AbilityInfo abilityInfo,string content,int level)
    {
        switch(abilityInfo.abilityCommands.Count)
        {
            case 1:
                content = string.Format(content,abilityInfo.abilityCommands[0].amountForLevel[level+1]);
                break;
            case 2:
                content = string.Format(content,abilityInfo.abilityCommands[0].amountForLevel[level+1],abilityInfo.abilityCommands[1].amountForLevel[level+1]);
                break;
            case 3:
                content = string.Format(content,abilityInfo.abilityCommands[0].amountForLevel[level+1],abilityInfo.abilityCommands[1].amountForLevel[level+1],abilityInfo.abilityCommands[2].amountForLevel[level+1]);
                break;
        }
        return content;
    }
}

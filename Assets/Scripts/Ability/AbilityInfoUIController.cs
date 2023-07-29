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
    private TMP_Text AbilityNametext;

    [SerializeField]
    private TMP_Text AbilityDescriptiontext;

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

        if (PlayerPrefs.HasKey(LocalizationManager.userSelectedLanguageKey) == false || PlayerPrefs.GetInt(LocalizationManager.userSelectedLanguageKey) == 0)
        {
            this.AbilityNametext.text = abilityInfo.abilityName.koreanName;
            this.AbilityDescriptiontext.text = abilityInfo.abilityDescription.koreanDescription;
        }
        else {
            this.AbilityNametext.text = abilityInfo.abilityName.englishName;
            this.AbilityDescriptiontext.text = abilityInfo.abilityDescription.englishDescription;
        }

    }
}

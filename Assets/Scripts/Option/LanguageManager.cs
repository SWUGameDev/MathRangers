using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{

    public TextMeshProUGUI languageTexts;
    private bool isKorean;
    bool isChanging = false;
    int selectedLanguageIndex;

    void Start()
    {
        isKorean = true;
        languageTexts.text = "한국어";
    }
    public void LanguageChange()
    {
        isKorean = !isKorean;
        if(isKorean)
        {
            languageTexts.text = "한국어";
            this.selectedLanguageIndex = 1;
            ChangeLocale(1);

        }
        else
        {
            languageTexts.text = "English";
            this.selectedLanguageIndex = 0;
            ChangeLocale(0);
        }
    }

    public void ChangeLocale(int index)
    {
        if (isChanging)
            return;

        StartCoroutine(LocaleChange(index));
    }

    IEnumerator LocaleChange(int index)
    {
        isChanging = true;

        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        PlayerPrefs.SetInt(LocalizationManager.userSelectedLanguageKey, this.selectedLanguageIndex);
        PlayerPrefs.Save();
        isChanging = false;
    }
}

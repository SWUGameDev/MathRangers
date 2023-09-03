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
            ChangeLocale(1);
        }
        else
        {
            languageTexts.text = "English";
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

        isChanging = false;
    }
}

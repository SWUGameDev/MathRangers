using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviourSingleton<LocalizationManager> {


// 0 English
// 1 Korean
    private bool isChanging = false; 

    public static readonly string userSelectedLanguageKey = "SelectedLanguage";
    public void ChangeLocalizationSetting(int selectedLanguageIndex,UnityAction onCompleted = null)
    {
        if(this.isChanging)
            return;

        this.isChanging = true;
        this.StartCoroutine(this.ChangeLocalizationSettingRoutine(selectedLanguageIndex,onCompleted));
    }

    public int GetCurrentLocalizationIndex()
    {
        if(PlayerPrefs.HasKey(LocalizationManager.userSelectedLanguageKey))
        {
            return PlayerPrefs.GetInt(LocalizationManager.userSelectedLanguageKey);
        }else{
            return -1;
        }
    }

    public static bool IsSettingKorean()
    {
        return (PlayerPrefs.GetInt(LocalizationManager.userSelectedLanguageKey) == 1) ;
    }
    
    private IEnumerator ChangeLocalizationSettingRoutine(int selectedLanguageIndex,UnityAction onCompleted = null)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[selectedLanguageIndex];
        PlayerPrefs.SetInt(LocalizationManager.userSelectedLanguageKey,selectedLanguageIndex);
        PlayerPrefs.Save();
        this.isChanging = false;
        onCompleted?.Invoke();
    }
}


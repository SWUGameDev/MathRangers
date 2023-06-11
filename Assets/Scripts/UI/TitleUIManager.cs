using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private string sceneName = GlobalSettings.LOGIN_SCENE_NAME;

    [SerializeField] private GameObject touchText;
    [SerializeField] private GameObject languageSelectPanel;
    [SerializeField] private GameObject changeSceneButton;

    [SerializeField] private List<Button> languageButtons;

    private bool isChanging = false; 

    private bool isChanged = false;

    private void Awake() {

        if(PlayerPrefs.HasKey("SelectedLanguage"))
        {
            this.SetTouchToStartUI(true);
            this.StartCoroutine(this.ChangeLocalizationSetting(PlayerPrefs.GetInt("SelectedLanguage")));
        }else{
            this.SetTouchToStartUI(false);
        }
    }

    public void SelectLanguageButton(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        if(this.isChanging == false)    
            this.StartCoroutine(this.ChangeLocalizationSetting(index));
    }

    private IEnumerator ChangeLocalizationSetting(int selectedLanguageIndex)
    {
        this.isChanging = true;

        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[selectedLanguageIndex];
        PlayerPrefs.SetInt("SelectedLanguage",selectedLanguageIndex);

        this.isChanging = false;
        this.isChanged = true;
        this.SetTouchToStartUI(true);
    }

    public void ChangeScene()
    {
        if(this.isChanged == false)
            return;

        SceneManager.LoadScene(this.sceneName);
    }

    private void SetTouchToStartUI(bool isActive)
    {
        this.touchText.SetActive(isActive);
        this.changeSceneButton.SetActive(isActive);
        this.languageSelectPanel.SetActive(!isActive);
    }

}

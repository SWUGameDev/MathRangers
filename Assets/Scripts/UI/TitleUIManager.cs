using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private string sceneName = GlobalSettings.MAIN_SCENE_NAME;

    [SerializeField] private GameObject touchText;
    [SerializeField] private GameObject languageSelectPanel;
    [SerializeField] private GameObject changeSceneButton;

    [SerializeField] private List<Button> languageButtons;

    private bool isChanged = false;

    private void Awake() {

    }

    private void Start() {
        if(FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserId() == null)
        {
            this.SetLanguagePanelActive();
        }else{

        }
    }

    private void SetLanguagePanelActive()
    {
        if(PlayerPrefs.HasKey(LocalizationManager.userSelectedLanguageKey))
        {
            this.SetTouchToStartUI(true);
            int selectedLanguageIndex = PlayerPrefs.GetInt(LocalizationManager.userSelectedLanguageKey);
            LocalizationManager.Instance.ChangeLocalizationSetting(selectedLanguageIndex,this.IsLanguageSelectedComplete);
        }else{
            this.SetTouchToStartUI(false);
        }
    }

    public void SelectLanguageButton(int index)
    {
        LocalizationManager.Instance.ChangeLocalizationSetting(index,this.ChangeSceneBeforeFirstSelectedLanguage);
    }

    private void IsLanguageSelectedComplete()
    {
        this.isChanged = true;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(this.sceneName);
    }

    private void ChangeSceneBeforeFirstSelectedLanguage()
    {
        SceneManager.LoadScene(this.sceneName);
    }

    private void SetTouchToStartUI(bool isActive)
    {
        this.touchText.SetActive(isActive);
        this.changeSceneButton.SetActive(isActive);
        this.languageSelectPanel.SetActive(!isActive);
    }

}

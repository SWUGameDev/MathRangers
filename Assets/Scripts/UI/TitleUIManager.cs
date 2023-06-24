using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject touchText;
    [SerializeField] private GameObject languageSelectPanel;

    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject changeSceneButton;

    [SerializeField] private List<Button> languageButtons;

    private void Start() {

        if(FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserId() == "")
        {
            this.SetLanguagePanelActive();
        }else{
            this.SetTouchToStartUI(true);
        }
    }

    private void SetLanguagePanelActive()
    {
        if(PlayerPrefs.HasKey(LocalizationManager.userSelectedLanguageKey))// 이미 존재한다면
        {
            this.SetLoginPanelActive(true);
        
            int selectedLanguageIndex = PlayerPrefs.GetInt(LocalizationManager.userSelectedLanguageKey);
            LocalizationManager.Instance.ChangeLocalizationSetting(selectedLanguageIndex,this.IsLanguageSelectedComplete);
        }else{
            this.languageSelectPanel.SetActive(true);
        }
    }

    private void SetLoginPanelActive(bool isActive)
    {
        this.touchText.SetActive(!isActive);
        this.changeSceneButton.SetActive(!isActive);
        this.languageSelectPanel.SetActive(!isActive);
        this.loginPanel.SetActive(isActive);
    }

    public void SelectLanguageButton(int index)
    {
        LocalizationManager.Instance.ChangeLocalizationSetting(index,this.ChangeSceneToLoginScene);
    }

    private void IsLanguageSelectedComplete()
    {
        this.SetLoginPanelActive(true);
    }

    public void ChangeSceneToMainScene()
    {
        SceneManager.LoadScene("03_MainScene");
    }

    public void ChangeSceneToLoginScene()
    {
        SceneManager.LoadScene("02_LoginScene");
    }

    private void SetTouchToStartUI(bool isActive)
    {
        this.touchText.SetActive(isActive);
        this.changeSceneButton.SetActive(isActive);
        this.languageSelectPanel.SetActive(!isActive);
    }

}

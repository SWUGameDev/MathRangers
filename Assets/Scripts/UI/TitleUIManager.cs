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

    [SerializeField] private SceneController sceneController;

    private void Start()
    {
        SoundManager.Instance.ChangeBackgroundAudioSource(backgroundAudioSourceType.BGM_TITLE);
    }

    private void SetLoginPanelActive(bool isActive)
    {
        this.touchText.SetActive(!isActive);
        this.changeSceneButton.SetActive(!isActive);
        this.languageSelectPanel.SetActive(!isActive);
        this.loginPanel.SetActive(isActive);
    }

    private void SetLanguageSelectPanelActive(bool isActive)
    {
        this.languageSelectPanel.SetActive(isActive);
        this.touchText.SetActive(!isActive);
        this.changeSceneButton.SetActive(!isActive);
    }

    public void OnTouchToStartClicked()
    {
        if(FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserId() != "")
        {
            if(!PlayerPrefs.HasKey("NicknameSettingCompleted"))
            {
                this.sceneController.LoadNicknameSettingScene();
            }else if(!PlayerPrefs.HasKey("DiagnosticCompleted"))
            {
                this.sceneController.LoadDiagnosticScene();
            }else{
                this.sceneController.LoadMainScene();
            }
            
        }else{
            this.SetLanguageSelectPanelActive(true);
        }
    }

    public void SelectLanguageButton(int index)
    {
        LocalizationManager.Instance.ChangeLocalizationSetting(index,this.SetLoginPanelActive);
    }

    private void IsLanguageSelectedComplete()
    {
        this.SetLoginPanelActive(true);
    }


    private void SetLoginPanelActive()
    {
        this.SetLoginPanelActive(true);
    }


}

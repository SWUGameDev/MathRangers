using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BossSceneStop : MonoBehaviour
{
    [SerializeField] GameObject stopPanel;
    [SerializeField] GameObject backSpace;
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] private SceneController sceneController;

    private void Awake()
    {
        if(Time.timeScale == 0)
        {
            // TO DO : Ÿ�� ������ ����
            Time.timeScale = 1;
            Debug.Log("time scale log");
        }
    }

    public void GameStop()
    {
        stopPanel.SetActive(true);
        backSpace.SetActive(true);
        Time.timeScale = 0f;
        headerText.text = "�Ͻ� ����";
    }

    public void GameEnd()
    {
        stopPanel.SetActive(true);
        backSpace.SetActive(false);
        Time.timeScale = 0f;
        headerText.text = "���� ����";

    }

    public void ExitGameScene()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.ChangeBackgroundAudioSource(backgroundAudioSourceType.BGM_MAIN);
        sceneController.LoadMainScene();
    }

    public void PressBackspace()
    {
        stopPanel.SetActive(false);
        backSpace.SetActive(false);
        Time.timeScale = 1f;
    }
}

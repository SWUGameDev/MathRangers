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
            // TO DO : 타임 스케일 버그
            Time.timeScale = 1;
            Debug.Log("time scale log");
        }
    }

    public void GameStop()
    {
        stopPanel.SetActive(true);
        backSpace.SetActive(true);
        Time.timeScale = 0f;
        headerText.text = "일시 정지";
    }

    public void GameEnd()
    {
        stopPanel.SetActive(true);
        backSpace.SetActive(false);
        Time.timeScale = 0f;
        headerText.text = "게임 종료";

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

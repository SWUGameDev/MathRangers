using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossSceneStop : MonoBehaviour
{
    [SerializeField] GameObject stopPanel;
    [SerializeField] GameObject backSpace;
    [SerializeField] TextMeshProUGUI headerText;


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

    public void PressBackspace()
    {
        stopPanel.SetActive(false);
        backSpace.SetActive(false);
        Time.timeScale = 1f;
    }
}

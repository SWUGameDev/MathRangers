using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameResultInfoUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text dateText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text accuracyText;

    [SerializeField] private TMP_Text speedText;

    [SerializeField] private TMP_Text progressText;

    [SerializeField] private TMP_Text correctRateText;

    [SerializeField] private TMP_Text gameResultText;

    public void SetText(string[] contentList)
    {
        this.dateText.text = contentList[0];
        this.scoreText.text = contentList[1];
        this.accuracyText.text = contentList[2];
        this.speedText.text = contentList[3];
        this.progressText.text = contentList[4];
        this.correctRateText.text = contentList[5];
        this.gameResultText.text = contentList[6];
    }
}

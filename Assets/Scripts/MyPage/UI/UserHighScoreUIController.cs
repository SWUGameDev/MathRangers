using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UserHighScoreUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    void Start()
    {
        this.SetHighScoreText();
    }

    private void SetHighScoreText()
    {
        string scoreData = PlayerPrefs.GetString(GameResultUIController.UserHighScoreKey);
        long score = scoreData == "" ? 0 : long.Parse(scoreData);
        this.scoreText.text = score.ToString("N0");
    }

}

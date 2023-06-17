using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("03_MainScene");
    }


    public void LoadRankingScene()
    {
        SceneManager.LoadScene("04_RankingScene");
    }
}

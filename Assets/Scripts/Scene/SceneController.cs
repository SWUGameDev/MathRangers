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

    public void LoadLoginScene()
    {
        SceneManager.LoadScene("02_LoginScene");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("01_TitleScene");
    }

    public void LoadNicknameSettingScene()
    {
        SceneManager.LoadScene("03_NicknameSettingScene");
    }

    public void LoadDiagnosticScene()
    {
        SceneManager.LoadScene("04_DiagnosticScene");
    }

    public void LoadSignupScene()
    {
        SceneManager.LoadScene("03_SignupScene");
    }

    public void LoadRunningScene()
    {
        SceneManager.LoadScene("07_RunScene");
    }

    public void LoadBossScene()
    {
        SceneManager.LoadScene("07_BossScene");
    }

    public void LoadShopScene()
    {
        SceneManager.LoadScene("06_ShopScene");
    }

    public void LoadMyPageScene()
    {
        SceneManager.LoadScene("08_MyPageScene");
    }
}

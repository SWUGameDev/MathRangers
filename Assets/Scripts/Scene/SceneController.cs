using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    [SerializeField] SceneTransitionAnimController sceneTransitionAnimController;
    public void LoadMainScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("03_MainScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("03_MainScene");});
    }


    public void LoadRankingScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("04_RankingScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("04_RankingScene");});
    }

    public void LoadLoginScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("02_LoginScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("02_LoginScene");});
    }

    public void LoadTitleScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("01_TitleScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("01_TitleScene");});
    }

    public void LoadNicknameSettingScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("03_NicknameSettingScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("03_NicknameSettingScene");});
    }

    public void LoadDiagnosticScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("04_DiagnosticScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("04_DiagnosticScene");});
    }

    public void LoadSignupScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("03_SignupScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("03_SignupScene");});
    }

    public void LoadRunningScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("07_RunScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("07_RunScene");});
    }

    public void LoadBossScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("07_BossScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("07_BossScene");});
    }

    public void LoadShopScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("06_ShopScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("06_ShopScene");});
    }

    public void LoadMyPageScene()
    {
        if(sceneTransitionAnimController == null)
            SceneManager.LoadScene("08_MyPageScene");
        else
            this.sceneTransitionAnimController.InOut(()=>{SceneManager.LoadScene("08_MyPageScene");});
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class LoginUIManager : MonoBehaviour
{
    public static LoginUIManager Instance;

    private void Awake()
    {
        this.waitForSeconds = new WaitForSeconds(this.popupTime);

        this.noticeObjectPool = new ObjectPool(this.noticePrefab,1,"Notice");

        LoginUIManager.Instance = this;
    }

    public void LoadSignupScene()
    {
        SceneManager.LoadScene("03_SignupScene");
    }

}

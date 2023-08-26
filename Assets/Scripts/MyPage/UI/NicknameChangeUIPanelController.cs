using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NicknameChangeUIPanelController : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;

    [SerializeField] private TMP_InputField nicknameInputField;

    [SerializeField] private TMP_Text nicknameField;

    [SerializeField] private NicknameDisplayUIController nicknameDisplayUIController;

    private string nickname;

    public void OpenNicknameChangePanel()
    {
        this.transform.gameObject.SetActive(true);
    }

    public void CloseNicknameChangePanel()
    {
        this.transform.gameObject.SetActive(false);
    }

    public void OpenNicknameConfirmPanel()
    {
        this.confirmPanel.SetActive(true);
    }

    public void CloseNicknameConfirmPanel()
    {
        this.confirmPanel.SetActive(false);
    }

    public void Start()
    {
        NicknameUIManager.OnNicknameConfirmed += this.OpenPanel;
    }

    private void OpenPanel(bool isChecked)
    {
        if(isChecked)
        {
            this.nicknameField.text = this.nicknameInputField.text;

            this.nickname = this.nicknameInputField.text;

            this.OpenNicknameConfirmPanel();
        }
    }

    public void ClickConfirmNicknameInConfirmPanel()
    {
        this.SaveNickname();

        this.CloseNicknameConfirmPanel();

        this.CloseNicknameChangePanel();

        this.nicknameDisplayUIController.SetNicknameText(this.nickname);

    }

    private void SaveNickname()
    {
            UserInfo userInfo = new UserInfo(FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserEmail(),"",this.nickname,-1);

            PlayerPrefs.SetString(NicknameUIManager.NicknamePlayerPrefsKey,this.nickname);

            string serializedData = JsonUtility.ToJson(userInfo);

            //FirebaseRealtimeDatabaseManager.Instance.UploadUserInfo();
    }
}

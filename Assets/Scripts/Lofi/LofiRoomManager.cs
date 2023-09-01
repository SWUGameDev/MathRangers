using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LofiRoomManager : MonoBehaviour
{
    [SerializeField] GameObject lofiSettingPanel;

    private void Start()
    {
        SoundManager.Instance.SetBackgroundAudioSourceMute(true);
    }

    public void LofiSettingPanelOpen()
    {
        lofiSettingPanel.SetActive(true);

    }

    public void LofiSettingPanelClose() 
    { 
        lofiSettingPanel.SetActive(false); 
    }


}

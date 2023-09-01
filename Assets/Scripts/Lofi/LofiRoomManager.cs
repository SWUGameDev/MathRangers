using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LofiRoomManager : MonoBehaviour
{
    [SerializeField] GameObject lofiSettingPanel;
    [SerializeField] Sprite[] moring;
    [SerializeField] Sprite[] night;
    [SerializeField] Image[] target;
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

    public void SetMorning()
    {
        for(int i = 0; i < 4; i++) 
        {
            target[i].sprite = moring[i];
        }
    }

    public void SetNight() 
    {
        for (int i = 0; i < 4; i++)
        {
            target[i].sprite = night[i];
        }
    }

    public void SetDog()
    {
        target[4].sprite = night[4];
    }

    public void SetCat()
    {
        target[4].sprite = moring[4];
    }
}

using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject optionsPanel;

    public void OptionsPanelOpen()
    {
        optionsPanel.SetActive(true);   
    }

    public void OptionsPanelClose()
    {
        optionsPanel.SetActive(false);
    }

    public void BackgroundSoundOn()
    {
        AudioListener.volume = 1;
    }

    public void BackgroundSoundOff()
    {
        AudioListener.volume = 0;
    }
}

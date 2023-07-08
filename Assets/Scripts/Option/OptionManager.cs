using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject blackScreen;

    private bool isEffectsMuted = false;
    private bool isBackgroundMuted = false;
    private bool isAllMuted = false;

    public void OptionsPanelOpen()
    {
        optionsPanel.SetActive(true); 
        blackScreen.SetActive(true);
    }

    public void OptionsPanelClose()
    {
        optionsPanel.SetActive(false);
        blackScreen.SetActive(false);
    }

    public void ToggleEffects()
    {
        isEffectsMuted = !isEffectsMuted;

        SoundManager.Instance.SetEffectAudioSourceMute(isEffectsMuted);
    }

    public void ToggleBackground()
    {
        isBackgroundMuted = !isBackgroundMuted;
        
        SoundManager.Instance.SetBackgroundAudioSourceMute(isBackgroundMuted);
    }

    public void ToggleEffectsAndMusic()
    {
        isAllMuted = !isAllMuted;

        isEffectsMuted = isAllMuted;
        isBackgroundMuted = isAllMuted; 
        SoundManager.Instance.SetBackgroundAudioSourceMute(isBackgroundMuted);
        SoundManager.Instance.SetEffectAudioSourceMute(isEffectsMuted);
    }
}

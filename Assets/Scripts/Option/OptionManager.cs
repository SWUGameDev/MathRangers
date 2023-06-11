using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject blackScreen;

    // 음악 재생 관리
    public AudioSource backgroundAudioSource;
    public AudioSource effectsAudioSource;

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
        effectsAudioSource.mute = isEffectsMuted;
    }

    public void ToggleBackground()
    {
        isBackgroundMuted = !isBackgroundMuted;
        backgroundAudioSource.mute = isBackgroundMuted;
    }

    public void ToggleEffectsAndMusic()
    {
        isAllMuted = !isAllMuted;

        isEffectsMuted = isAllMuted;
        isBackgroundMuted = isAllMuted; 
        backgroundAudioSource.mute = isBackgroundMuted;
        effectsAudioSource.mute = isEffectsMuted;
    }
}

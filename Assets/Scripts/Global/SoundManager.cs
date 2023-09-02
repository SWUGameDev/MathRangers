using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum backgroundAudioSourceType
{
    BGM_MAIN,
    BGM_BOSS_BATTLE,
    BGM_RUN,
    BGM_TITLE,
    BGM_PROLOGUE,
    BGM_RANKING,
    BGM_LOFI1,
    BGM_RAIN,
    BGM_TALK,
}

public enum effectsAudioSourceType
{
    SFX_TIME_LIMIT_TEN_REMAINED,
    SFX_DIAGNOSTIC_O,
    SFX_DIAGNOSTIC_X,
    SFX_JUMP1,
    SFX_JUMP2,
    SFX_SWING,
    SFX_BOSS_BEHIT,
    SFX_RUSH,
    SFX_PLAYER_ATTACK,
    SFX_BOSS_CALL,
    SFX_SELECT_ABILITY,
    SFX_POPUP,
    SFX_CHEESE,
    SFX_HURDLE,
    SFX_TYPING,
    SFX_CLICK,
    SFX_CHARACTER_CLICK,
    SFX_SUCCESS,
    SFX_FAIL,
}

[Serializable]
public class BackgroundAudioClipInfo{
    public backgroundAudioSourceType backgroundAudioType;
    public AudioClip audioClip;
}

[Serializable]
public class AffectAudioClipInfo{
    public effectsAudioSourceType effectAudioType;
    public AudioClip audioClip;
}


public class SoundManager : MonoBehaviourSingleton<SoundManager>
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectsAudioSource;

    [SerializeField] List<BackgroundAudioClipInfo> backgroundAudioClipArray;

    private Dictionary<backgroundAudioSourceType,AudioClip> backgroundAudioClipDictionary;

    [SerializeField] List<AffectAudioClipInfo> effectAudioClipArray;

    private Dictionary<effectsAudioSourceType,AudioClip> effectAudioDictionary;

    public void PlayAffectSoundOneShot(effectsAudioSourceType type,float volumeScale)
    {
        if(this.effectAudioDictionary == null)
            this.ChangeSFXArrayToDictionary();

        this.effectsAudioSource.PlayOneShot(this.effectAudioDictionary[type],volumeScale);
    }

    public void StopEffectsAudioSource()
    {
        this.effectsAudioSource.Stop();
    }


    public void PlayAffectSoundOneShot(effectsAudioSourceType type)
    {
        if(this.effectAudioDictionary == null)
            this.ChangeSFXArrayToDictionary();

        this.effectsAudioSource.PlayOneShot(this.effectAudioDictionary[type]);
    }

    public void PlayAffectSound(effectsAudioSourceType type)
    {
        if(this.effectAudioDictionary == null)
            this.ChangeSFXArrayToDictionary();
    }

    public void StopEffectAudioSource()
    {
        this.effectsAudioSource.Stop();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_CLICK);
        }
    }

    public void ChangeBackgroundAudioSource(backgroundAudioSourceType type)
    {
        if(this.backgroundAudioClipDictionary == null)
            this.ChangeBGMArrayToDictionary();
        
        this.backgroundAudioSource.clip = this.backgroundAudioClipDictionary[type];
        this.backgroundAudioSource.Play();
    }

    public void SetEffectAudioSourceMute(bool isEffectsMuted)
    {
        this.effectsAudioSource.mute = isEffectsMuted;
    }


    public void SetBackgroundAudioSourceMute(bool isBackgroundMuted)
    {
        this.backgroundAudioSource.mute = isBackgroundMuted;
    }

    private void ChangeBGMArrayToDictionary()
    {
        this.backgroundAudioClipDictionary = new Dictionary<backgroundAudioSourceType, AudioClip>();
        foreach (var backgroundAudioClipInfo in this.backgroundAudioClipArray)
        {
            this.backgroundAudioClipDictionary[backgroundAudioClipInfo.backgroundAudioType] = backgroundAudioClipInfo.audioClip;
        }
    }

    private void ChangeSFXArrayToDictionary()
    {
        this.effectAudioDictionary = new Dictionary<effectsAudioSourceType, AudioClip>();
        foreach (var affectAudioClipInfo in this.effectAudioClipArray)
        {
            this.effectAudioDictionary[affectAudioClipInfo.effectAudioType] = affectAudioClipInfo.audioClip;
        }
    }



}

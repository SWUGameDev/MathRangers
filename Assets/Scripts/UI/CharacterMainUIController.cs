using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMainUIController : MonoBehaviour
{
    public void OnCharacterClicked()
    {
        SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_CHARACTER_CLICK);
    }
}

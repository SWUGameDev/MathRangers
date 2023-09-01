using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class LofiSoundManager : MonoBehaviour
{

    [SerializeField] AudioMixer BGMMixer;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider RainSlider;
    [SerializeField] Slider TalkSlider;


    public void BGMControl()
    {
        float sound = BGMSlider.value;
        if (sound == -40f) BGMMixer.SetFloat("BGM", -80);
        else BGMMixer.SetFloat("BGM", sound);
    }

    public void RainControl()
    {
        float sound = RainSlider.value;
        if (sound == -40f) BGMMixer.SetFloat("Rain", -80);
        else BGMMixer.SetFloat("Rain", sound);
    }

    public void TalkControl()
    {
        float sound = TalkSlider.value;
        if (sound == -40f) BGMMixer.SetFloat("Talk", -80);
        else BGMMixer.SetFloat("Talk", sound);
    }
}

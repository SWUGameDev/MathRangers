using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

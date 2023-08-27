using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class RandomTalkController : MonoBehaviour
{
    [SerializeField] private TMP_Text talkText;

    [SerializeField] private TextAsset engTextAsset;

    [SerializeField] private List<string> engTalkContents;

    [SerializeField] private TextAsset krTextAsset;

    [SerializeField] private List<string> krTalkContents;
    void Start()
    {

        this.engTalkContents = JsonConvert.DeserializeObject<List<string>>(this.engTextAsset.text);

        this.krTalkContents = JsonConvert.DeserializeObject<List<string>>(this.krTextAsset.text);
        
        if(LocalizationManager.IsSettingKorean())
        {
            this.talkText.text = this.krTalkContents[Random.Range(0,this.krTalkContents.Count)];
        }else{
            this.talkText.text = this.engTalkContents[Random.Range(0,this.engTalkContents.Count)];
        }
    }
}

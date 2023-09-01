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

    private float elapsedTime;

    private int index;

    private int maxIndex;

    private int languageIndex = 0;

    [SerializeField] private float timeInterval;
    void Start()
    {

        this.engTalkContents = JsonConvert.DeserializeObject<List<string>>(this.engTextAsset.text);

        this.krTalkContents = JsonConvert.DeserializeObject<List<string>>(this.krTextAsset.text);
        
        this.maxIndex = this.engTalkContents.Count;

        this.index = Random.Range(0,this.maxIndex);

        this.languageIndex = LocalizationManager.Instance.GetCurrentLocalizationIndex();

        if(this.languageIndex == 1)
        {
            this.talkText.text = this.krTalkContents[this.index];
        }else{
            this.talkText.text = this.engTalkContents[this.index];
        }
    }

    private void Update()
    {
        this.elapsedTime += Time.deltaTime;

        if(this.timeInterval<=this.elapsedTime)
        {
            this.elapsedTime = 0;

            this.index = (this.index + 1) % this.maxIndex;

            if(this.languageIndex == 1)
            {
                this.talkText.text = this.krTalkContents[this.index];
            }else{
                this.talkText.text = this.engTalkContents[this.index];
            }
        }
    }
}

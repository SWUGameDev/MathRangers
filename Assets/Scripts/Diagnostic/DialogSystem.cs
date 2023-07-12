using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private TextAsset textAsset;

    private List<DialogData> dialogData;

    [Header("Dialog UI")]

    [SerializeField]
    private DialogSystemUIInfo leftCharacter;

        [SerializeField]
    private DialogSystemUIInfo rightCharacter;


    void Start()
    {

        this.InitializeDialogScriptData();

        this.SetDialogUI(0);

    }

    private void SetDialogUI(int index)
    {
        DialogSystemUIInfo selectedUIInfo;
        selectedUIInfo = this.dialogData[index].activeTalker == 0 ? this.leftCharacter : this.rightCharacter;

        selectedUIInfo.contentText.text = this.dialogData[index].content;
        selectedUIInfo.talkerNameText.text = this.dialogData[index].talkerName;
        selectedUIInfo.characterImage.sprite = selectedUIInfo.characterSprites[this.dialogData[index].spriteType];
    }

    

    // TODO: 성능이슈로 csv to json 고려해보기
    private void InitializeDialogScriptData()
    {
        List<Dictionary<string, object>> scriptData = CSVReader.Read(textAsset);
        this.dialogData = new List<DialogData>();
        for(int i=0; i < scriptData.Count; i++) {
            this.dialogData.Add(new DialogData((int)scriptData[i]["talker"],Convert.ToBoolean(scriptData[i]["selection"]),scriptData[i]["name"].ToString(),(int)scriptData[i]["spriteType"],scriptData[i]["content"].ToString() ));
        }
    }

}

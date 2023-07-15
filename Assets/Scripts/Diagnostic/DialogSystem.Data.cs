using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public partial class DialogSystem : MonoBehaviour
{

    [Header("Data Sheet")]
    
    [SerializeField]
    private TextAsset[] TextAssets;

    private List<List<string>> selectPanelData;
    [SerializeField]
    private TextAsset[] SelectPanelTextAssets;

    private List<List<DialogData>> dialogData;

    // TODO: 성능이슈로 csv to json 고려해보기
    private void InitializeDialogScriptData()
    {
        this.dialogData = new List<List<DialogData>>();
        foreach(TextAsset textAsset in this.TextAssets)
        {
            List<Dictionary<string, object>> scriptData = CSVReader.Read(textAsset);

            var dialogData = new List<DialogData>();
            
            for(int i=0; i < scriptData.Count; i++) {
                dialogData.Add(new DialogData((int)scriptData[i]["talker"],Convert.ToBoolean(scriptData[i]["selection"]),scriptData[i]["name"].ToString(),(int)scriptData[i]["spriteType"],scriptData[i]["content"].ToString() ));
            }

            this.dialogData.Add(dialogData);
        }

    }


    private void InitializeSelectedPanelScriptData()
    {


    }

}

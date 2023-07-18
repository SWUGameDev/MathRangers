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
    private TextAsset[] EnglishTextAssets;
    
    [SerializeField]
    private TextAsset[] TextAssets;

    private List<List<SelectInfoData>> selectPanelData;
    
    [SerializeField]
    private TextAsset[] EnglishSelectPanelTextAssets;

    [SerializeField]
    private TextAsset[] SelectPanelTextAssets;

    private List<List<DialogData>> dialogData;

    private void InitializeTextAssets(int index)
    {
        if(index == 0)
        {
            this.TextAssets = this.EnglishTextAssets;
            this.SelectPanelTextAssets = this.EnglishSelectPanelTextAssets;
        }
    }

    private void InitializeDialogScriptData()
    {
        this.dialogData = new List<List<DialogData>>();
        foreach(TextAsset textAsset in this.TextAssets)
        {
            List<Dictionary<string, object>> scriptData = CSVReader.Read(textAsset);

            var dialogData = new List<DialogData>();
            
            for(int i=0; i < scriptData.Count; i++) {
                dialogData.Add(new DialogData((int)scriptData[i]["talker"],Convert.ToBoolean(scriptData[i]["selection"]),scriptData[i]["name"].ToString(),(int)scriptData[i]["spriteType"],(int)scriptData[i]["isMultiContent"],scriptData[i]["content"].ToString() ));
            }

            this.dialogData.Add(dialogData);
        }
    }


    private void InitializeSelectedPanelScriptData()
    {
        this.selectPanelData = new List<List<SelectInfoData>>();
        foreach(TextAsset textAsset in this.SelectPanelTextAssets)
        {
            List<Dictionary<string, object>> scriptData = CSVReader.Read(textAsset);

            var selectInfoData = new List<SelectInfoData>();
            for(int i=0; i < scriptData.Count; i++) {
                List<SelectInfoData.SelectInfo> selectInfoList = new List<SelectInfoData.SelectInfo>();
                selectInfoList.Add(new SelectInfoData.SelectInfo((int)scriptData[i]["select1Type"],scriptData[i]["select1"].ToString()));
                selectInfoList.Add(new SelectInfoData.SelectInfo((int)scriptData[i]["select2Type"],scriptData[i]["select2"].ToString()));
                selectInfoList.Add(new SelectInfoData.SelectInfo((int)scriptData[i]["select3Type"],scriptData[i]["select3"].ToString()));
                
                selectInfoData.Add(new SelectInfoData(selectInfoList));
            }
            this.selectPanelData.Add(selectInfoData);
        }
    }

}

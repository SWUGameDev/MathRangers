using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public partial class DialogSystem : MonoBehaviour
{

    [Header("Dialog UI")]

    [SerializeField]
    private DialogSystemUIInfo leftCharacter;

    [SerializeField]
    private DialogSystemUIInfo rightCharacter;

    [SerializeField]
    private Color unActiveCharacterColor;
    [SerializeField]
    private Color activeCharacterColor;

    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private Button[] selectedButtons;
    [SerializeField]
    private TMP_Text[] selectedTexts;

    [SerializeField]
    private GameObject selectedPanel;

    [SerializeField]
    private float textAnimationDuration;

    [SerializeField]
    private GameObject dialogCanvas;

    [SerializeField]
    private GameObject diagnosticCanvas;

    private int dataIndex = 0;

    private int dialogDataIndex = 0;

    private int selectedPanelIndex = 0;

    private TeamMatchManager teamMatchManager;

    public static Action<int> onDialogEnded;

    private string userNickname;

    private int languageIndex;

    private void Awake() {
        this.userNickname = PlayerPrefs.GetString(NicknameUIManager.NicknamePlayerPrefsKey);
        this.userNickname = this.userNickname == null? "Unknown" : this.userNickname;
    }


    void Start()
    {
        this.languageIndex = LocalizationManager.Instance.GetCurrentLocalizationIndex();

        this.InitializeTextAssets(this.languageIndex);

        this.teamMatchManager = TeamMatchManager.GetInstance();
        
        this.StartCoroutine(this.FadeIn());

        this.InitializeDialogScriptData();

        this.InitializeSelectedPanelScriptData();

        this.SetDialogUI(this.dataIndex,this.dialogDataIndex);

    }

    public void DialogNextButtonClicked()
    {
        if(this.dialogDataIndex + 1 >=this.dialogData[this.dataIndex].Count)
        {
            DialogSystem.onDialogEnded?.Invoke(this.dataIndex);
            return;
        }

        this.dialogDataIndex++;

        if(this.dialogData[this.dataIndex][this.dialogDataIndex-1].isSelection)
        {
            this.SetSelectPanelUnActive();
        }

        this.SetDialogUI(this.dataIndex,this.dialogDataIndex);
    }

    public void SetDialogCanvasActive()
    {
        this.dataIndex++;
        this.dialogDataIndex = 0;

        this.SetDialogUI(this.dataIndex,this.dialogDataIndex);

        this.diagnosticCanvas.SetActive(false);
        this.dialogCanvas.SetActive(true);
    }

    private void SetDialogUI(int dataIndex,int index)
    {
        DialogSystemUIInfo selectedUIInfo = this.dialogData[dataIndex][index].activeTalker == 0 ? this.leftCharacter : this.rightCharacter;
        DialogSystemUIInfo opponentUIInfo = this.dialogData[dataIndex][index].activeTalker == 0 ? this.rightCharacter : this.leftCharacter;

        //발화자 UI Setting
        selectedUIInfo.talkerNamePanel.SetActive(true);
        selectedUIInfo.characterImage.color = this.activeCharacterColor;

        if(this.languageIndex == 0)
            selectedUIInfo.contentText.text = this.dialogData[dataIndex][index].content.Replace("[Nickname]",this.userNickname);
        else
            selectedUIInfo.contentText.text = this.dialogData[dataIndex][index].content.Replace("[닉네임]",this.userNickname);
        selectedUIInfo.talkerNameText.text = this.dialogData[dataIndex][index].talkerName;
        selectedUIInfo.characterImage.sprite = selectedUIInfo.characterSprites[this.dialogData[dataIndex][index].spriteType];

        //상대방 UI Setting
        opponentUIInfo.talkerNamePanel.SetActive(false);
        opponentUIInfo.characterImage.color = this.unActiveCharacterColor;

        // 만약에 셀렉트 패널을 선택하는 경우라면
        if(this.dialogData[dataIndex][index].isSelection)
        {
            this.nextButton.interactable = false;
            this.SetSelectPanel(this.selectedPanelIndex);
            this.selectedPanel.SetActive(true);
        }

        //만약에 선택지가 여러개라면
        if(this.dialogData[dataIndex][index].isMultiContent>0)
        {
            List<string> contents = new List<string>();
            contents.Add(this.dialogData[dataIndex][this.dialogDataIndex].content);
            while(this.dialogDataIndex + 1 < this.dialogData[this.dataIndex].Count && this.dialogData[dataIndex][this.dialogDataIndex+1].isMultiContent!=0)
            {
                this.dialogDataIndex++;
                contents.Add(this.dialogData[dataIndex][this.dialogDataIndex].content);
            }
            this.SetTextInMultipleContents(selectedUIInfo,contents,this.teamMatchManager.GetSelectedTeam());
        }

        this.DoText(selectedUIInfo.contentText);
    }

    private void SetTextInMultipleContents(DialogSystemUIInfo selectedUIInfo,List<string> contents,int index)
    {
        if(this.languageIndex == 0)
            selectedUIInfo.contentText.text = this.dialogData[dataIndex][index].content.Replace("[Nickname]",this.userNickname);
        else
            selectedUIInfo.contentText.text = this.dialogData[dataIndex][index].content.Replace("[닉네임]",this.userNickname);
    }

    private void SetSelectPanel(int selectedPanelIndex)
    {
        if(this.selectPanelData[this.dataIndex].Count <= selectedPanelIndex)
            return;

        SelectInfoData selectInfoData = this.selectPanelData[this.dataIndex][this.selectedPanelIndex];
        for(int index = 0;index<3;index++)
        {
            this.selectedTexts[index].text = selectInfoData.selectInfoList[index].content;
        }

        this.selectedPanelIndex++;
    }

    public void SetSelectPanelUnActive()
    {
        this.selectedPanel.SetActive(false);
        this.nextButton.interactable = true;
    }

    public void SelectButton(int index)
    {
        SelectInfoData selectInfoData = this.selectPanelData[this.dataIndex][this.selectedPanelIndex-1];
        int teamType = selectInfoData.selectInfoList[index].type;
        this.teamMatchManager.SetTeamMatchScore((TeamType)teamType - 1,1);
    }

}

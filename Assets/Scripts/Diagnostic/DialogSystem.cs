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
    private GameObject selectedPanel;

    [SerializeField]
    private float textAnimationDuration;

    private int dataIndex = 0;

    private int index = 0;

    private int selectedPanelIndex = 0;


    void Start()
    {
        this.StartCoroutine(this.FadeIn());

        this.InitializeDialogScriptData();

        this.SetDialogUI(this.dataIndex,this.index);

    }

    public void DialogNextButtonClicked()
    {
        if(this.index + 1 >=this.dialogData[this.dataIndex].Count)
            return;

        this.index++;
        this.SetDialogUI(this.dataIndex,this.index);
    }

    private void SetDialogUI(int dataIndex,int index)
    {
        DialogSystemUIInfo selectedUIInfo = this.dialogData[dataIndex][index].activeTalker == 0 ? this.leftCharacter : this.rightCharacter;
        DialogSystemUIInfo opponentUIInfo = this.dialogData[dataIndex][index].activeTalker == 0 ? this.rightCharacter : this.leftCharacter;

        //발화자 UI Setting
        selectedUIInfo.talkerNamePanel.SetActive(true);
        selectedUIInfo.characterImage.color = this.activeCharacterColor;

        selectedUIInfo.contentText.text = this.dialogData[dataIndex][index].content;
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

        this.DoText(selectedUIInfo.contentText);
    }

    private void SetSelectPanel(int selectedPanelIndex)
    {
        //TODO : Panel Data Setting
    }

    public void SetSelectPanelUnActive()
    {
        this.selectedPanel.SetActive(false);
        this.nextButton.interactable = true;
    }


    



}

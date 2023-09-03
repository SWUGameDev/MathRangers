using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StudyUIPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text totalQuestionsText;

    private int maxQuestionCount = 8;
    void Start()
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetStudyPanelData;
        UserGameResultInfoManager.OnUserGameResultInfoInitialized += SetStudyPanelData;
    }

    private void OnDestroy() {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetStudyPanelData;
    }

    private void SetStudyPanelData(List<GameResultInfo> gameResultInfos)
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetStudyPanelData;

        int totalCount = gameResultInfos.Count * this.maxQuestionCount;
        this.SetTotalQuestionsText(totalCount.ToString());
    }

    private void SetTotalQuestionsText(string totalCount)
    {
        this.totalQuestionsText.text = totalCount;
    }
}

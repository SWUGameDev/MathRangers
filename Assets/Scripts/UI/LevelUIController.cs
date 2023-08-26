using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;

    [SerializeField] private TMP_Text expText;

    [SerializeField] private Slider expSlider;

    private int maxQuestionCount = 8;

    private int[] countForLevelUp = new int[]{0,35,120,400,700}; 

    void Start()
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetLevelUIData;
        UserGameResultInfoManager.OnUserGameResultInfoInitialized += SetLevelUIData;  
    }

    private void SetLevelUIData(List<GameResultInfo> gameResultInfos)
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetLevelUIData;

        int count = gameResultInfos.Count * this.maxQuestionCount;
        int level = 0;

        for(int index = 0;index<this.countForLevelUp.Length;index++)
        {
            if(count<this.countForLevelUp[index])
            {
                level = index;
                break;
            }
        }

        this.levelText.text = $"Lv {level}";

        // 최고 레벨일 경우 예외처리 따로 필요함
        if(level<countForLevelUp.Length)
        {
            this.expText.text = $"{count}/{this.countForLevelUp[level]}";

            this.expSlider.value = count/(float)this.countForLevelUp[level];
        }

    }

}

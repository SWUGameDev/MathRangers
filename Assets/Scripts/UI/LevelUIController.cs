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

    void Start()
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetLevelUIData;
        UserGameResultInfoManager.OnUserGameResultInfoInitialized += SetLevelUIData;  
    }

    private void SetLevelUIData(List<GameResultInfo> gameResultInfos)
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetLevelUIData;

    }

}

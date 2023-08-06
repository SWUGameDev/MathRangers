using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WjChallenge;
public partial class GameResultUIController : MonoBehaviour
{
    private void SetNPCContent(GameResultType type,string lrnPrgsStsCd)
    {
        if(LocalizationManager.Instance.GetCurrentLocalizationIndex() == 1)
        {
            switch(lrnPrgsStsCd)
            {
                case "LPSC01":
                    this.npcChatText.text = this.gameResultContentDictionary[type].gameResultContentList[0].DescriptionKorean;
                    break;
                case "LPSC02":
                    this.npcChatText.text = this.gameResultContentDictionary[type].gameResultContentList[1].DescriptionKorean;
                    break;
                case "LPSC03":
                    this.npcChatText.text = this.gameResultContentDictionary[type].gameResultContentList[2].DescriptionKorean;
                    break;
                case "LPSC04":
                    this.npcChatText.text = this.gameResultContentDictionary[type].gameResultContentList[3].DescriptionKorean;
                    break;
            }
        }else{
            switch(lrnPrgsStsCd)
            {
                case "LPSC01":
                    this.npcChatText.text = this.gameResultContentDictionary[type].gameResultContentList[0].DescriptionEnglish;
                    break;
                case "LPSC02":
                    this.npcChatText.text = this.gameResultContentDictionary[type].gameResultContentList[1].DescriptionEnglish;
                    break;
                case "LPSC03":
                    this.npcChatText.text = this.gameResultContentDictionary[type].gameResultContentList[2].DescriptionEnglish;
                    break;
                case "LPSC04":
                    this.npcChatText.text = this.gameResultContentDictionary[type].gameResultContentList[3].DescriptionEnglish;
                    break;
            }
        }

    }

    private void SetResponseLearningProgressData(Response_Learning_ProgressData progressData)
    {
        this.explanationResultText.text = $"{progressData.explAcrcyRt}%";

        switch(progressData.acrcyCd)
        {
            case "A":
                this.accuracyText.text = "완벽";
                break;
            case "B":
                this.accuracyText.text = "높음";
                break;
            case "C":
                this.accuracyText.text = "보통";
                break;
            case "D":
                this.accuracyText.text = "미달";
                break;
            default:
                this.accuracyText.text = "None";
                break;
        }

        switch(progressData.explSpedCd)
        {
            case "ESC01":
                this.explanationSpeedText.text = "느림";
                break;
            case "ESC02":
                this.explanationSpeedText.text = "보통";
                break;
            case "ESC03":
                this.explanationSpeedText.text = "빠름";
                break;
            default:
                this.explanationSpeedText.text = "None";
                break;
        }
    }
}

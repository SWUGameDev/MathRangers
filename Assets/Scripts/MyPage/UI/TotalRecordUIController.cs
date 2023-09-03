using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TotalRecordUIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button button;
    void Start()
    {
        this.button.interactable = false;

        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetButtonInteractable;
        UserGameResultInfoManager.OnUserGameResultInfoInitialized += SetButtonInteractable;  
    }

    private void OnDestroy() {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetButtonInteractable;
    }

    private void SetButtonInteractable(List<GameResultInfo> gameResultInfos)
    {
        UserGameResultInfoManager.OnUserGameResultInfoInitialized -= SetButtonInteractable;
        this.button.interactable = true;
    }

}

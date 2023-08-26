using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelTotalUIController : MonoBehaviour
{
    [SerializeField] private UserGameResultInfoManager userGameResultInfoManager;

    [SerializeField] private GameObject recordPanel;
    [SerializeField] private GameObject statisticsPanel;
    [SerializeField] private Button recordButton;
    [SerializeField] private Image recordButtonImage;
    [SerializeField] private Button statisticsButton;
    [SerializeField] private Image statisticsButtonImage;
    [SerializeField] private Sprite yellowSprite;
    [SerializeField] private Sprite graySprite;

    private bool isStatisticsActive = false;

    void Start()
    {
        this.recordButton.onClick.AddListener(()=>{
            if(isStatisticsActive==true)
            {
                this.isStatisticsActive = false;
                this.recordPanel.SetActive(true);
                this.statisticsPanel.SetActive(false);
                this.recordButtonImage.sprite = this.yellowSprite;
                this.statisticsButtonImage.sprite = this.graySprite;
            }
        });

        this.statisticsButton.onClick.AddListener(()=>{
            if(isStatisticsActive==false)
            {
                this.isStatisticsActive = true;
                this.recordPanel.SetActive(false);
                this.statisticsPanel.SetActive(true);
                this.recordButtonImage.sprite = this.graySprite;
                this.statisticsButtonImage.sprite = this.yellowSprite;
            }
        });
    }

    public void SetTotalPanelActive()
    {
        this.transform.gameObject.SetActive(true);
    }

    public void SetTotalPanelUnActive()
    {
        this.transform.gameObject.SetActive(false);
    }

}

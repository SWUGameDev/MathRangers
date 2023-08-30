using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUIInfo : MonoBehaviour
{
    [SerializeField] public ItemInfo itemInfo { get; private set;}

    [SerializeField] private ShopUIManager shopUIManager;

    [SerializeField] private Button itemButton;

    [SerializeField] private Image lockImage;

    [SerializeField] private GameObject bottomLayout;

    [SerializeField] private TMP_Text PriceText;

    [SerializeField] private GameObject SelectedPanel;

    private void Awake() {

        this.itemInfo = this.transform.GetComponent<ItemInfo>();

        int playerLevel = PlayerPrefManager.GetInt(PlayerPrefManager.PlayerLevelKey);
        playerLevel = playerLevel == 0 ? 1: playerLevel;

        this.SetPanelLockInfo(playerLevel);

        this.PriceText.text = this.itemInfo.price.ToString();

        this.itemButton.onClick.AddListener(()=>{
            this.shopUIManager.SelectItem(this);
        });
    }

    private void Start()
    {
        if(this.shopUIManager.IsOwnedItem(this.itemInfo.itemId))
        {
            this.bottomLayout.SetActive(false);
        }

    }

    private void SetPanelLockInfo(int playerLevel)
    {
        if(this.itemInfo.minLevel <= playerLevel)
        {
            this.lockImage.gameObject.SetActive(false);
        }else{
            this.itemInfo.isLocked = true;
        }
    }

    public void SetBottomUILayoutActive(bool isActive)
    {
        this.bottomLayout.SetActive(isActive);
    }

    public void SetSelectedPanel(bool isActive)
    {
        this.SelectedPanel.SetActive(isActive);
    }
}

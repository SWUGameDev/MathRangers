using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIInfo : MonoBehaviour
{
    [SerializeField] public ItemInfo itemInfo { get; private set;}

    [SerializeField] private ShopUIManager shopUIManager;

    [SerializeField] private Button itemButton;

    [SerializeField] private GameObject SelectedPanel;

    private void Awake() {
        this.itemInfo = this.transform.GetComponent<ItemInfo>();

        this.itemButton.onClick.AddListener(()=>{
            this.shopUIManager.SelectItem(this);
        });
    }

    public void SetSelectedPanel(bool isActive)
    {
        this.SelectedPanel.SetActive(isActive);
    }
}

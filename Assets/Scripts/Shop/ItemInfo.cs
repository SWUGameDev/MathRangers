using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HEAD,
    BACK
}

public class ItemInfo : MonoBehaviour
{
    [SerializeField] public int itemId;
    [SerializeField] public string itemResourceFileName;
    [SerializeField] public int price;
    [SerializeField] public ItemType itemType;
    [SerializeField] public int minLevel;
    [SerializeField] private bool isOwned;
}

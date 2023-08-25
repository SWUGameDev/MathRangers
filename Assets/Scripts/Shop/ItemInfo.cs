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
    [SerializeField] private int itemId;
    [SerializeField] private string itemResourceFileName;
    [SerializeField] public int price {get; private set;}
    [SerializeField] public ItemType itemType;
    [SerializeField] public int minLevel;
    [SerializeField] private bool isOwned;
}

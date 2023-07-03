using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HEAD,
    TOP,
    BOTTOM,
    SHOE
}
public class ItemInfo : MonoBehaviour
{
    [SerializeField] private int itemId;
    [SerializeField] public int price {get; private set;}
    [SerializeField] public ItemType itemType {get; private set;}

    [SerializeField] private bool isOwned;
}

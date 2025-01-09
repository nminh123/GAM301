using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public byte stack;
    public ItemSO itemData;

    public InventoryItem(ItemSO _itemData)
    {
        itemData = _itemData;
    }

    public void AddStack() => stack++;
    public void RemoveStack() => stack--;
}

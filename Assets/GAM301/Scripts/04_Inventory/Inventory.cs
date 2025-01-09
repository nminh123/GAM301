using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<InventoryItem> inventory;
    private Dictionary<ItemSO, InventoryItem> inventoryDic;
    private void Start()
    {
        
        inventory = new();
        inventoryDic = new();
    }
    private void Update()
    {
        Application.targetFrameRate = 60;
    }
    public void AddItem(ItemSO _item)
    {
        if(inventoryDic.TryGetValue(_item, out InventoryItem value))
            value.AddStack();
        else
        {
            var newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDic.Add(_item, newItem);
        }
    }
    public void RemoveItem(ItemSO _item)
    {
        if (inventoryDic.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stack <= 1)
            {
                inventory.Remove(value);
                inventoryDic.Remove(_item);
            }
            else
                value.RemoveStack();
        }
    }
}

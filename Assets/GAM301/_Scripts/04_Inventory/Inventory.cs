using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<InventoryItem> inventory;
    private Dictionary<ItemSO, InventoryItem> inventoryDic;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlots;
    private ItemSlot[] itemSlot;
    public GameObject UI;
    private void Start()
    {     
        inventory = new();
        inventoryDic = new();

        itemSlot = inventorySlots.GetComponentsInChildren<ItemSlot>();
        UI.gameObject.SetActive(false);
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
        UpdateSlotsUI();
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
    
    private void UpdateSlotsUI()
    {
        for(int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].CleanSlot();
        }

        for(int i = 0; i < inventory.Count; i++)
        {
            itemSlot[i].UpdateSlot(inventory[i]);
        }
    }

    public void IsOpen(bool _isOpen)
    {
        if (_isOpen)
            UI.gameObject.SetActive(true);
        else
            UI.gameObject.SetActive(false);
    }
}

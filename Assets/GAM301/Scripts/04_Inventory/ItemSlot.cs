using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] protected Image itemIcon;
    [SerializeField] protected TextMeshProUGUI itemText;

    [SerializeField] protected InventoryItem item;

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;
        itemIcon.color = Color.white;

        if (item != null)
        {
            itemIcon.sprite = item.itemData.itemIcon;
            if (item.stack > 1)
                itemText.text = item.stack.ToString();
            else
                itemText.text = "";
        }
    }

    public void CleanSlot()
    {
        item = null;

        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        itemText.text = "";
    }
}

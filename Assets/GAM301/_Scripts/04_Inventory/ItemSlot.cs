using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image itemIcon;
    [SerializeField] protected TextMeshProUGUI itemText;
    [SerializeField] protected Image count;
    [SerializeField] protected InventoryItem item;

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;
        itemIcon.color = Color.white;

        if (item != null)
        {
            itemIcon.sprite = item.itemData.itemIcon;
            if (item.stack > 1)
            {
                itemText.text = item.stack.ToString();
                count.gameObject.SetActive(true);
            }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item == null) 
            return;

        GameManager.Instance.toolTipManager.ShowToolTip(item.itemData.description, item.itemData.nameItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.toolTipManager.HideToolTip();
    }
}

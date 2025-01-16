public class InventoryItem
{
    public byte stack;
    public ItemSO itemData;

    public InventoryItem(ItemSO _itemData)
    {
        itemData = _itemData;
        AddStack();
    }

    public void AddStack() => stack++;
    public void RemoveStack() => stack--;
}

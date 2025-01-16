using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName = "Data/Item")]
public class ItemSO : ScriptableObject
{
    public byte Id;
    public string nameItem;
    public string description;
    public Sprite itemIcon;
}

using UnityEngine;

[CreateAssetMenu(fileName ="Data/Item",menuName ="New Item")]
public class ItemSO : MonoBehaviour
{
    public byte Id;
    public string nameItem;
    public string description;
    public Sprite itemIcon;
}

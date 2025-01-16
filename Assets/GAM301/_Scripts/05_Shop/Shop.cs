using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public ItemSO item;
        public int price;
    }

    [Header("Shop Items")]
    [SerializeField] private List<ShopItem> shopItems;


    public void BuyItem(ItemSO itemToBuy)
    {
        var gameManager = GameManager.Instance;
        if(gameManager == null ) return;
        ShopItem shopItem = shopItems.Find(s => s.item == itemToBuy);
        if (shopItem != null)
        {
            if (gameManager.moneyManager.TakeMoney() >= shopItem.price)
            {
                gameManager.moneyManager.RemoveMoney(shopItem.price);

                gameManager.inventoryManager.AddItem(itemToBuy);

            }
        }
    }
}

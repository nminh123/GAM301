using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopFucus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject fucosGameObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        fucosGameObject.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fucosGameObject.gameObject.SetActive(false);
    }
}

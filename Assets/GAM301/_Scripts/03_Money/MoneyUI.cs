using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI moneyText;
    [SerializeField] MoneyManager moneyManager;
    
    private void Start()
    {
        moneyText.text = moneyManager.TakeMoney().ToString();
    }

    private void OnEnable()
    {
        moneyManager.OnMoneyChanged += UpdateUI;
    }

    private void OnDisable()
    {
        moneyManager.OnMoneyChanged -= UpdateUI;
    }

    protected virtual void UpdateUI(int currentMoney)
    {
        moneyText.text = currentMoney.ToString();
    }
}

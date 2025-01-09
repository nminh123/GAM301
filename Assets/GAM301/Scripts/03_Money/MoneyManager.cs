using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int money;
    public int TakeMoney() => money;
    public void AddMoney(int _money)
    {
        money += _money;
    }
    public void RemoveMoney(int _money)
    {
        if(money < _money || money < 0) 
            return;

        money -= _money;
    }
}

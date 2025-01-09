using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance;
    public static GameManager Instance { get => instance; }
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public MoneyManager moneyManager;    
    private void Reset()
    {
        moneyManager = GetComponent<MoneyManager>();
    }
}

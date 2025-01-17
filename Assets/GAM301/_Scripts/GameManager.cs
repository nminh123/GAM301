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
    public Inventory inventoryManager;
    public SoundManager soundManager;
    public ToolTipManager toolTipManager;
    private void Reset()
    {
        moneyManager = GetComponentInChildren<MoneyManager>();
        inventoryManager = GetComponentInChildren<Inventory>();
        soundManager = GetComponentInChildren<SoundManager>();
        toolTipManager = GetComponentInChildren<ToolTipManager>();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWallet
{
    public event Action<int> MoneyCountChanged;
    public int Money { get; }
    public void AddMoney(int amount);
    public bool TrySpendMoney(int amount);
}

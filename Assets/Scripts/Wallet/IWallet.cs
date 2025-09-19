using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWallet
{
    public void AddMoney(int amount);
    public bool TrySpendMoney(int amount);
}

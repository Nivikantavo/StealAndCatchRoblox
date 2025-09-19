using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBuyMediator
{
    public bool CanExecuteAction(Player player, int price)
    {
        if(player.HasHoldersAtHouse == false)
        {
            Debug.Log("No free holders in the house.");
            return false;
        }
        if(player.Wallet.TrySpendMoney(price) == false)
        {
            Debug.Log("Not enough money to buy the mob.");
            return false;
        }
        else
        {
            Debug.Log("Mob bought successfully.");
            return true;
        }
    }
}

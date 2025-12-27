using UnityEngine;

public class InGameWallet : Wallet
{
    public InGameWallet(IPersistenData persistenData, Currency currency = Currency.InGameCoins) : base(persistenData, currency)
    {
        Debug.Log(persistenData);
        Debug.Log(persistenData.UserData);
        Debug.Log(persistenData.UserData.InLastGameCoins);
        _balance = persistenData.UserData.InLastGameCoins;
        MoneyCountChangedCall(_balance);
    }

    protected override void SaveValue()
    {
        _persistenData.UserData.InLastGameCoins = _balance;
    }
}

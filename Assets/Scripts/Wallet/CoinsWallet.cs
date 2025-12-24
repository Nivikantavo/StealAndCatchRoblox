public class CoinsWallet : Wallet
{
    public CoinsWallet(IPersistenData persistenData, Currency currency = Currency.Coins) : base(persistenData, currency)
    {
        _balance = persistenData.UserData.Coins;
        MoneyCountChangedCall(_balance);
    }

    protected override void SaveValue()
    {
        _persistenData.UserData.Coins = _balance;
    }
}

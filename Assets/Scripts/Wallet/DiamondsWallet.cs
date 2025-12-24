public class DiamondsWallet : Wallet
{
    public DiamondsWallet(IPersistenData persistenData, Currency currency = Currency.Diamonds) : base(persistenData, currency)
    {
        _balance = persistenData.UserData.Diamonds;
        MoneyCountChangedCall(_balance);
    }

    protected override void SaveValue()
    {
        _persistenData.UserData.Diamonds = _balance;
    }
}

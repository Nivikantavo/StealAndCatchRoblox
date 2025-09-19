public class Wallet : IWallet
{
    public int Money => _money;

    private int _money = 0;

    public Wallet(int startingMoney)
    {
        _money = startingMoney;
    }

    public void AddMoney(int amount)
    {
        if (amount < 0) return;
        _money += amount;
    }

    public bool TrySpendMoney(int amount)
    {
        if (amount < 0) return false;
        if (_money >= amount)
        {
            _money -= amount;
            return true;
        }
        return false;
    }
}

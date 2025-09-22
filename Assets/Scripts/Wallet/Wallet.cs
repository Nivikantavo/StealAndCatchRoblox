using System;

public class Wallet : IWallet
{
    public int Money => _money;

    private int _money = 0;

    public event Action<int> MoneyCountChanged;

    public Wallet(int startingMoney)
    {
        _money = startingMoney;
        MoneyCountChanged?.Invoke(_money);
    }

    public void AddMoney(int amount)
    {
        if (amount < 0) return;
        _money += amount;
        MoneyCountChanged?.Invoke(_money);
    }

    public bool TrySpendMoney(int amount)
    {
        if (amount < 0) return false;
        if (_money >= amount)
        {
            _money -= amount;
            MoneyCountChanged?.Invoke(_money);
            return true;
        }
        return false;
    }
}

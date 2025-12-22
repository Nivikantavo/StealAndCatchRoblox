using System;

public class Wallet : IWallet
{
    public int Balance => _balance;

    private int _balance = 0;

    public event Action<int> MoneyCountChanged;

    public Wallet(int startingMoney)
    {
        _balance = startingMoney;
        MoneyCountChanged?.Invoke(_balance);
    }

    public void AddMoney(int amount)
    {
        if (amount < 0) return;
        _balance += amount;
        MoneyCountChanged?.Invoke(_balance);
    }

    public bool TrySpendMoney(int amount)
    {
        if (amount < 0) return false;
        if (_balance >= amount)
        {
            _balance -= amount;
            MoneyCountChanged?.Invoke(_balance);
            return true;
        }
        return false;
    }
}

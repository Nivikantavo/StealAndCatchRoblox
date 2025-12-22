using System;

public abstract class Wallet
{
    public int Balance => _balance;

    protected int _balance = 0;

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

    public bool IsEnough(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        return _balance < amount;
    }

    public void Spend(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        _balance -= amount;
        MoneyCountChanged?.Invoke(_balance);
    }
}

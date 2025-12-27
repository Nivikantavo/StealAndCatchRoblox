using System;

public abstract class Wallet
{
    public int Balance => _balance;
    public Currency Currency => _currency;

    public event Action<int> MoneyCountChanged;

    protected int _balance = 0;
    protected Currency _currency;
    protected IPersistenData _persistenData;

    public Wallet(IPersistenData persistenData, Currency currency)
    {
        _persistenData = persistenData;
        _currency = currency;
    }

    public void AddMoney(int amount)
    {
        if (amount < 0) return;
        _balance += amount;
        MoneyCountChangedCall(_balance);
        SaveValue();
    }

    public bool IsEnough(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        return _balance >= amount;
    }

    public void Spend(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        _balance -= amount;
        MoneyCountChangedCall(_balance);
        SaveValue();
    }

    protected void MoneyCountChangedCall(int newValue)
    {
        MoneyCountChanged?.Invoke(newValue);
    }

    protected abstract void SaveValue();

}

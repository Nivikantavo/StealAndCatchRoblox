using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Player : MonoBehaviour
{
    public IInteractor Interactor => _interactor;
    public IWallet Wallet => _wallet;
    public IStealer Stealer => _mobStealer;

    protected IInteractor _interactor;
    protected House _house;
    protected Wallet _wallet;
    protected MobStealer _mobStealer;

    public MobHolder GetFreeMobHolder()
    {
        return _house.GetFreeHolder();
    }

    public virtual void Initialize(House house)
    {
        _house = house;
        _interactor = GetComponent<IInteractor>();
        _interactor.Initialize(this, _house.transform);
        _mobStealer = GetComponent<MobStealer>();
        _mobStealer.Initialize(_interactor);
        _wallet = new Wallet(1000);
    }

    public void TakeHit()
    {
        _mobStealer.LoseMob();
    }
}

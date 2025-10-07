using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Player : MonoBehaviour
{
    public IInteractor Interactor => _interactor;
    public bool HasHoldersAtHouse => _house.HasFreeHolder;
    public IWallet Wallet => _wallet;

    protected IInteractor _interactor;
    protected House _house;
    protected Wallet _wallet;

    public MobHolder GetFreeMobHolder()
    {
        return _house.GetFreeHolder();
    }

    public virtual void Initialize(House house)
    {
        _house = house;
        _interactor = GetComponent<IInteractor>();
        _interactor.Initialize(this, _house.transform);
        _wallet = new Wallet(1000);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    public bool HasHoldersAtHouse => _house.HasFreeHolder;
    public IWallet Wallet => _wallet;

    [SerializeField] private PlayerInteractor _playerInteractor;
    [SerializeField] private House _house;

    private Wallet _wallet;

    private void Awake()
    {
        Construct(_house);
    }

    [Inject]
    private void Construct(House house)
    {
        _house = house;
        _playerInteractor.Initialize(this, _house.transform);
        _wallet = new Wallet(1000);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHouseInstaller : MonoInstaller
{
    [SerializeField] private PlayerHouse _playerHouse;

    public override void InstallBindings()
    {
        BindPlayerHouse();
    }

    private void BindPlayerHouse()
    {
        Container.Bind<PlayerHouse>().FromInstance(_playerHouse).AsSingle().NonLazy();
    }
}

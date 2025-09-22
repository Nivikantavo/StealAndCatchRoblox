using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHousInstaller : MonoInstaller
{
    [SerializeField] private House _playerHouse;

    public override void InstallBindings()
    {
        BindPlayerHouse();
    }

    private void BindPlayerHouse()
    {
        Container.Bind<House>().FromInstance(_playerHouse).AsSingle();
    }
}

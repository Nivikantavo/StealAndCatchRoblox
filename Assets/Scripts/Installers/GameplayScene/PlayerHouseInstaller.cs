using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHouseInstaller : MonoInstaller
{
    [SerializeField] private PlayerHouse _playerHouse;
    [SerializeField] private List<HousePlace> _housePlaces;

    public override void InstallBindings()
    {
        BindPlayerHouse();
    }

    private void BindPlayerHouse()
    {
        Container.Bind<PlayerHouse>().FromInstance(_playerHouse).AsSingle().NonLazy();
        int randomIndex = Random.Range(0, _housePlaces.Count);
        _playerHouse.transform.position = _housePlaces[randomIndex].transform.position;
        _playerHouse.transform.rotation = _housePlaces[randomIndex].transform.rotation;
        _housePlaces[randomIndex].SetHouse(_playerHouse);
    }
}

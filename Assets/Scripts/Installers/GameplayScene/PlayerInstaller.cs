using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private InteractableView _interactableView;
    [SerializeField] private Transform _playerSpawnPosition;

    public override void InstallBindings()
    {
        BindInteractView();
        BindPlayer();
    }

    private void BindPlayer()
    {
        Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerSpawnPosition.position, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<Player>().FromInstance(player).AsSingle();
    }

    private void BindInteractView()
    {
        InteractableView interactableView = Container.InstantiatePrefabForComponent<InteractableView>(_interactableView, _playerSpawnPosition.position, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<InteractableView>().FromInstance(interactableView).AsSingle();
    }
}

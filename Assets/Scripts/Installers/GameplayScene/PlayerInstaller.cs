using Cinemachine;
using ECM2.Examples;
using ECM2.Walkthrough.Ex92;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private UserPlayer _playerPrefab;
    [SerializeField] private InteractableView _interactableView;
    [SerializeField] private SimpleCameraController _cameraController;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public override void InstallBindings()
    {
        BindInteractView();
        BindPlayer();
    }

    private void BindCamera(Transform target)
    {
        _cameraController.target = target;
        _virtualCamera.Follow = target;
    }

    private void BindPlayer()
    {
        UserPlayer player = Container.InstantiatePrefabForComponent<UserPlayer>(_playerPrefab);
        Container.BindInterfacesAndSelfTo<UserPlayer>().FromInstance(player).AsSingle();

        BindCamera(player.GetCameraPivot());
        player.GetComponent<PlayerCharacterController>().camera = Camera.main;
        player.GetComponent<ThirdPersonController>().followCamera = _virtualCamera;
    }

    private void BindInteractView()
    {
        InteractableView interactableView = Container.InstantiatePrefabForComponent<InteractableView>(_interactableView);
        Container.BindInterfacesAndSelfTo<InteractableView>().FromInstance(interactableView).AsSingle();
    }
}

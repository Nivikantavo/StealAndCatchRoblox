using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

public class LevelStarterInstaller : MonoInstaller
{
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private List<HousePlace> _housesPositions;
    public override void InstallBindings()
    {
        Container.Bind<NavMeshSurface>().FromInstance(_navMeshSurface).AsSingle();
        Container.Bind<List<HousePlace>>().FromInstance(_housesPositions).AsSingle();
        BindLevelStarter();
    }

    private void BindLevelStarter()
    {
        Container.Bind<LevelStarter>().FromNewComponentOnNewGameObject().WithGameObjectName("LevelStarter").AsSingle().NonLazy();
    }
}

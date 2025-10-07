using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class BotsInstaller : MonoInstaller
{
    [SerializeField] private BotPlayer _botTemplate;
    [SerializeField] private BotsHouse _botHouseTemplate;
    [SerializeField] private List<Transform> _housesPositions;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    public override void InstallBindings()
    {
        BindBots();
    }

    private void BindBots()
    {
        for (int i = 0; i < _housesPositions.Count; i++)
        {
            BotsHouse spawnedHouse = Container.InstantiatePrefabForComponent<BotsHouse>(_botHouseTemplate, _housesPositions[i].position, _housesPositions[i].rotation, null);
            BotPlayer botPlayer = Container.InstantiatePrefabForComponent<BotPlayer>(_botTemplate, spawnedHouse.OwnerSpawnPosition.position, Quaternion.identity, null);
            spawnedHouse.Initialzie(botPlayer, i + 8);
        }

        _navMeshSurface.BuildNavMesh();
    }
}

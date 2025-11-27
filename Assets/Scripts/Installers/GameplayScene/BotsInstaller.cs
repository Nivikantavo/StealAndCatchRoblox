using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class BotsInstaller : MonoInstaller
{
    [SerializeField] private BotPlayer _botTemplate;
    [SerializeField] private BotsHouse _botHouseTemplate;
    [SerializeField] private List<HousePlace> _housesPositions;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private int _botsPlayersCount;

    public override void InstallBindings()
    {
        BindBots();
    }

    private void BindBots()
    {
        for (int i = 0; i < _housesPositions.Count - 1 && i < _botsPlayersCount; i++)
        {
            var housePlace = _housesPositions.FirstOrDefault(place => place.HasHouse == false);

            BotsHouse spawnedHouse = Container.InstantiatePrefabForComponent<BotsHouse>(_botHouseTemplate, housePlace.transform.position, housePlace.transform.rotation, null);
            housePlace.SetHouse(spawnedHouse);
            BotPlayer botPlayer = Container.InstantiatePrefabForComponent<BotPlayer>(_botTemplate, spawnedHouse.OwnerSpawnPosition.position, Quaternion.identity, null);
            spawnedHouse.Initialzie(botPlayer, i + 8);
        }
        
        _navMeshSurface.BuildNavMesh();
    }
}

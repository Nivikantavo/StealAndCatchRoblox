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
    [SerializeField] private SkinsPoolConfig _skinsPool;
    [SerializeField] private int _botsPlayersCount;

    public override void InstallBindings()
    {
        BindBots();
        BindBotsSkins();
    }

    private void BindBots()
    {
        var bots = new List<BotPlayer>();
        var houses = new List<BotsHouse>();
        for (int i = 0; i < _botsPlayersCount; i++)
        {
            BotsHouse spawnedHouse = Container.InstantiatePrefabForComponent<BotsHouse>(_botHouseTemplate);
            BotPlayer botPlayer = Container.InstantiatePrefabForComponent<BotPlayer>(_botTemplate);
            bots.Add(botPlayer);
            houses.Add(spawnedHouse);
        }

        Container.Bind<List<BotPlayer>>()
            .FromInstance(bots)
            .AsSingle();

        Container.Bind<List<BotsHouse>>()
                 .FromInstance(houses)
                 .AsSingle();
    }

    private void BindBotsSkins()
    {
        Container.Bind<SkinsPoolConfig>().FromScriptableObject(_skinsPool).AsSingle();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MobFactoryInstaller : MonoInstaller
{
    [SerializeField] private Transform _spawnedContainer;
    [SerializeField] private List<BrainrotMobConfig> _possibleAssets;
    [SerializeField] private BrainrotMob _mobTemplate;

    public override void InstallBindings()
    {
        Container.Bind<MobFactory>().FromMethod(context => 
        new MobFactory(_spawnedContainer, _possibleAssets, _mobTemplate)).AsSingle();
    }
}

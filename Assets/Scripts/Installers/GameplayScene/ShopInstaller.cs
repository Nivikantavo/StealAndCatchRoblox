using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class ShopInstaller : MonoInstaller
{
    [SerializeField] private ShopContent _shopContent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    private IPersistenData _persistenData;

    [Inject]
    private void Construct(IPersistenData persistenData)
    {
        _persistenData = persistenData;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_shopContent).AsSingle();
        Container.BindInstance(_shopItemViewFactory).AsSingle();

        BindSkinWorkers();
    }

    private void BindSkinWorkers()
    {
        Container.Bind<OpenSkinChecker>().FromMethod(context =>
        new OpenSkinChecker(_persistenData)).AsSingle();

        Container.Bind<SelectedSkinChecker>().FromMethod(context =>
        new SelectedSkinChecker(_persistenData)).AsSingle();

        Container.Bind<SkinSelector>().FromMethod(context =>
        new SkinSelector(_persistenData)).AsSingle();

        Container.Bind<SkinUnlocker>().FromMethod(context =>
        new SkinUnlocker(_persistenData)).AsSingle();
    }
}

using UnityEngine;
using Zenject;

public class UserInstaller : MonoInstaller
{
    private IDataProvider _dataProvider;
    private IPersistenData _persistenData;

    public override void InstallBindings()
    {
        BindData();
        BindWallets();
    }

    private void BindData()
    {
        _persistenData = new PersistenData();
        _dataProvider = new DataLocalProvider(_persistenData);

        Container.Bind<IPersistenData>().FromInstance(_persistenData).AsSingle().NonLazy();
        Container.Bind<IDataProvider>().FromInstance(_dataProvider).AsSingle().NonLazy();

        LoadOrInitData();
    }

    private void BindWallets()
    {
        Debug.Log("BindWallets");
        Container.Bind<InGameWallet>().FromMethod(context =>
        new InGameWallet(_persistenData)).AsSingle();

        Container.Bind<CoinsWallet>().FromMethod(context =>
        new CoinsWallet(_persistenData)).AsSingle();

        Container.Bind<DiamondsWallet>().FromMethod(context =>
        new DiamondsWallet(_persistenData)).AsSingle();
    }

    private void LoadOrInitData()
    {
        if (_dataProvider.TryLoad() == false)
            _persistenData.UserData = new UserData();
        Debug.Log("Data ready");
    }
}

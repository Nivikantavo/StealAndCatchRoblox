using UnityEngine;

public class BotInteractor : IInteractor
{
    public Transform SelfTransform { get; private set; }
    public Transform HouseTransform { get; private set; }
    public IWallet Wallet => throw new System.NotImplementedException();
    public MobHolder MobHolder => throw new System.NotImplementedException();

    public BotInteractor(Transform selfTransform, Transform houseTransform)
    {
        SelfTransform = selfTransform;
        HouseTransform = houseTransform;
    }
}

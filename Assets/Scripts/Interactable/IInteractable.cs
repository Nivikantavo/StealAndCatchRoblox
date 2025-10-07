using UnityEngine;

public interface IInteractable
{
    public int Price { get;}
    public Transform SelfTransform { get; }
    void Interact(IInteractor interactor);
}

public interface IInteractor
{
    public MobHolder MobHolder { get; }
    public Transform SelfTransform { get; }
    public Transform HouseTransform { get; }
    public IWallet Wallet { get; }
    public Transform CarryPosition { get; }

    public void Initialize(Player player, Transform houseTransform);
}
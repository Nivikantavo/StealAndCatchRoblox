using UnityEngine;

public interface IInteractable
{
    public int Price { get;}
    public IInteractor Stealer { get; }
    public Transform SelfTransform { get; }
    void Interact(IInteractor interactor);
    public IInteractor Owner { get; }
    public bool IsInteractable { get; }
}

public interface IInteractor
{
    public MobHolder MobHolder { get; }
    public Transform SelfTransform { get; }
    public Transform HouseTransform { get; }
    public InGameWallet Wallet { get; }
    public IStealer Stealer { get; }

    public void Initialize(Player player, Transform houseTransform);
}
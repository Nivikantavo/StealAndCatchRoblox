using UnityEngine;

public interface IInteractable
{
    public int Price { get;}
    public bool IsGraped { get; }
    public Transform SelfTransform { get; }
    void Interact(IInteractor interactor);
    public IInteractor Owner { get; }
}

public interface IInteractor
{
    public MobHolder MobHolder { get; }
    public Transform SelfTransform { get; }
    public Transform HouseTransform { get; }
    public IWallet Wallet { get; }
    public IStealer Stealer { get; }

    public void Initialize(Player player, Transform houseTransform);
}
using UnityEngine;

public interface IInteractable
{
    public InteractAction InteractAction { get; }
    public Transform SelfTransform { get; }
    void Interact(IInteractor interactor);
}

public interface IInteractor
{
    public Transform SelfTransform { get; }
    public Transform HouseTransform { get; }
    public IWallet Wallet { get; }
    public MobHolder MobHolder { get; }
}
using UnityEngine;

public interface IInteractable
{
    public Transform SelfTransform { get; }
    void Interact(IInteractor interactor);
}

public interface IInteractor
{
    public Transform SelfTransform { get; }
    public Transform HouseTransform { get; }
    public IWallet Wallet { get; }
    public Transform CarryPosition { get; }
}
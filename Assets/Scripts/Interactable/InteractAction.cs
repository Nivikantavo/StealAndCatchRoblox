using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractAction
{
    public InteractAction(InteractActionType actionType)
    {
        ActionType = actionType;
    }

    public InteractActionType ActionType { get; protected set; }
    public abstract void ExecuteAction(IInteractor interactor);
}

public enum InteractActionType
{
    Sell,
    Buy,
    Steal
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractAction
{
    public event Action ActionExecuted;
    protected virtual void ExecuteAction(IInteractor interactor)
    {
        ActionExecuted?.Invoke();
    }
    public abstract bool TryExecuteAction(IInteractor interactor);
}

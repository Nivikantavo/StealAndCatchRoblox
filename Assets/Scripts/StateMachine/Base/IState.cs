using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public bool IsInteractable { get; }
    public void Enter();
    public void Exit();
    public void Update();
    public void InputAction(IInteractor interactor);
}

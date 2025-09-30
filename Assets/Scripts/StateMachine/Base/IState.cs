using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    //public event Action InputActionInovked;
    public void Enter();
    public void Exit();
    public void Update();
    public void InputAction(IInteractor interactor);
}

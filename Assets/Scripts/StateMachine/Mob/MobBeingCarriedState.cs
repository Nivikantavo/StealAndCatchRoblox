using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBeingCarriedState : IState
{
    private readonly IStateSwitcher _stateSwitcher;
    private readonly MobStateData _mobStateData;

    public MobBeingCarriedState(IStateSwitcher stateSwitcher, MobStateData mobStateData)
    {
        _stateSwitcher = stateSwitcher;
        _mobStateData = mobStateData;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void InputAction(IInteractor interactor)
    {
        
    }

    public void Update()
    {
        if(_mobStateData.CurrentHolder != null && !_mobStateData.IsCarryng)
        {
            _stateSwitcher.SwitchState<MobWorkingState>();
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBeingCarriedState : IState
{
    private readonly IStateSwitcher _stateSwitcher;
    private readonly MobStateData _mobStateData;
    private readonly BrainrotMob _mob;

    public MobBeingCarriedState(IStateSwitcher stateSwitcher, MobStateData mobStateData, BrainrotMob mob)
    {
        _stateSwitcher = stateSwitcher;
        _mobStateData = mobStateData;
        _mob = mob;
    }

    public void Enter()
    {
        Debug.Log($"Enter {GetType()}");
        _mobStateData.StealerPlayer.Stealer.GrabMob(_mob);
    }

    public void Exit()
    {
        
    }

    public void InputAction(IInteractor interactor)
    {
        
    }

    public void Update()
    {
        if(_mobStateData.StealerPlayer == null)
        {
            _stateSwitcher.SwitchState<MobWorkingState>();
            return;
        }
    }
}

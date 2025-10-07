using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobWorkingState : IState
{
    private readonly IStateSwitcher _stateSwitcher;
    private readonly MobStateData _mobStateData;

    private float _elapsedTime = 0;
    private BrainrotMob _mob;
    private InteractAction _interactAction;

    public MobWorkingState(IStateSwitcher stateSwitcher, MobStateData mobStateData, BrainrotMob mob)
    {
        _stateSwitcher = stateSwitcher;
        _mobStateData = mobStateData;
        _mob = mob;
        _interactAction = new WorkingMob(_mobStateData.Owner, mob);
    }

    public void Enter()
    {
        Debug.Log($"Enter {GetType()}");
        _mob.SetOnHolder();
        _elapsedTime = 0;
    }

    public void Exit()
    {
        _mobStateData.CurrentHolder = null;
    }

    public void InputAction(IInteractor interactor)
    {
        _interactAction.TryExecuteAction(interactor);
    }

    public void Update()
    {
        if(_mobStateData.Stealer != null)
        {
            _stateSwitcher.SwitchState<MobBeingCarriedState>();
            return;
        }

        if (_mobStateData.IsSold)
        {
            _stateSwitcher.SwitchState<MobWalkingState>();
            return;
        }

        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= 1)
        {
            _mobStateData.CurrentHolder.AddMoney(_mob.Config.ValuePerSecond);
            _elapsedTime = 0;
        }
    }
}

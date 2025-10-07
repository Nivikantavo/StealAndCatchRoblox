using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobWorkingState : IState
{
    private readonly IStateSwitcher _stateSwitcher;
    private readonly MobStateData _mobStateData;

    private float _elapsedTime = 0;
    private BrainrotMob _mob;
    private InteractAction _saleAction;
    private InteractAction _stealAction;

    public MobWorkingState(IStateSwitcher stateSwitcher, MobStateData mobStateData, BrainrotMob mob)
    {
        _stateSwitcher = stateSwitcher;
        _mobStateData = mobStateData;
        _mob = mob;
        _saleAction = new SellableMob(_mobStateData, mob);
        _stealAction = new StealableMob(_mobStateData, mob);
    }

    public void Enter()
    {
        Debug.Log($"Enter {GetType()}");
        _mob.SetOnHolder();
        _elapsedTime = 0;
    }

    public void Exit()
    {
        _elapsedTime = 0;
    }

    public void InputAction(IInteractor interactor)
    {
        if(interactor == _mobStateData.Owner)
        {
            _saleAction.TryExecuteAction(interactor);
        }
        else
        {
            _stealAction.TryExecuteAction(interactor);
        }

    }

    public void Update()
    {
        if(_mobStateData.StealerPlayer != null)
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

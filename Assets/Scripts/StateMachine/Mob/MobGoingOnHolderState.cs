using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobGoingOnHolderState : IState
{
    private readonly IStateSwitcher _stateSwitcher;
    private readonly MobStateData _mobStateData;
    private BrainrotMob _mob;
    private InteractAction _saleAction;
    private NavMeshAgent _agent;

    public MobGoingOnHolderState(IStateSwitcher stateSwitcher, NavMeshAgent agent, MobStateData mobStateData, BrainrotMob mob)
    {
        _stateSwitcher = stateSwitcher;
        _mobStateData = mobStateData;
        _mob = mob;
        _agent = agent;
        _saleAction = new SellableMob(_mobStateData, _mob);
    }

    public void Enter()
    {
        //Debug.Log($"Enter {GetType()}");
        _agent.enabled = true;
        _agent.SetDestination(_mobStateData.CurrentHolder.transform.position);
    }

    public void Exit()
    {
        _agent.enabled = false;
    }

    public void InputAction(IInteractor interactor)
    {
        if(interactor == _mobStateData.Owner)
        {
            _saleAction.TryExecuteAction(interactor);
        }
    }

    public void Update()
    {
        if (Vector3.Distance(_mob.transform.position, _mobStateData.CurrentHolder.transform.position) < 1f)
        {
            _stateSwitcher.SwitchState<MobWorkingState>();
        }
    }
}

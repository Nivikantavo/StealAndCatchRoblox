using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobWalkingState : IState
{
    private readonly IStateSwitcher _stateSwitcher;
    private readonly MobStateData _mobStateData;
    private readonly NavMeshAgent _agent;

    private Vector3 _destinationPoint;
    private InteractAction _interactAction;

    public MobWalkingState(IStateSwitcher stateSwitcher, NavMeshAgent agent, MobStateData mobStateData, BrainrotMob mob)
    {
        _stateSwitcher = stateSwitcher;
        _agent = agent;
        _mobStateData = mobStateData;
        _interactAction = new PurchasableMob(mob);
    }

    public void Enter()
    {
        Debug.Log($"Enter {GetType()}");
        SetNewDestination(_mobStateData.Destination);
    }

    public void Exit()
    {
        _agent.isStopped = true;
        _agent.enabled = false;
    }

    public void InputAction(IInteractor interactor)
    {
        _interactAction.TryExecuteAction(interactor);
    }

    public void Update()
    {
        if(_destinationPoint != _mobStateData.Destination)
        {
            SetNewDestination(_mobStateData.Destination);
        }

        if(_mobStateData.CurrentHolder != null && !_mobStateData.IsCarryng)
        {
            _stateSwitcher.SwitchState<MobWorkingState>();
            return;
        }
    }

    private void SetNewDestination(Vector3 point)
    {
        _destinationPoint = point;
        _agent.SetDestination(_destinationPoint);
    }
}

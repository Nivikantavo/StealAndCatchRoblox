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
    private BrainrotMob _mob;

    public MobWalkingState(IStateSwitcher stateSwitcher, NavMeshAgent agent, MobStateData mobStateData, BrainrotMob mob)
    {
        _stateSwitcher = stateSwitcher;
        _agent = agent;
        _mobStateData = mobStateData;
        _mob = mob;
        _interactAction = new PurchasableMob(_mob);
    }

    public void Enter()
    {
        //Debug.Log($"Enter {GetType()}");
        _agent.enabled = true;
    }

    public void Exit()
    {
        _agent.enabled = false;
        _destinationPoint = Vector3.zero;
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

        if(_mobStateData.CurrentHolder != null && _mobStateData.StealerPlayer == null)
        {
            _stateSwitcher.SwitchState<MobGoingOnHolderState>();
            return;
        }
    }

    private void SetNewDestination(Vector3 point)
    {
        _destinationPoint = point;
        _agent.SetDestination(_destinationPoint);
    }
}

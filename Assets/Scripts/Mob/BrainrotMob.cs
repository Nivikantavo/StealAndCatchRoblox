using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BrainrotMob : MonoBehaviour, IInteractable
{
    public event Action Taked;
    public event Action Stolen;
    public BrainrotMobConfig Config => _config;
    public GameObject Model => _config.MobPrefab;
    public float Speed => _agent.speed;
    public int Price => Config.BaseCost;
    public IInteractor Stealer => _mobStateData.StealerPlayer;
    public Transform SelfTransform => transform;
    public IInteractor Owner => _mobStateData.Owner;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private MobInfoCanvas _mobInfoCanvas;

    private StateMachine _stateMachine;
    private MobStateData _mobStateData;
    private BrainrotMobConfig _config;

    private void Update()
    {
        _stateMachine.Update();
    }

    public void Initialize(BrainrotMobConfig config)
    {
        _config = config;
        Instantiate(_config.MobPrefab, transform);
        _mobInfoCanvas.Initialize(_config.Name, _config.BaseCost.ToString(), false);
        _mobStateData = new MobStateData();
        _stateMachine = new StateMachine();

        List<IState> states = new List<IState>()
        {
            new MobWalkingState(_stateMachine, _agent, _mobStateData, this),
            new MobGoingOnHolderState(_stateMachine, _agent, _mobStateData, this),
            new MobWorkingState(_stateMachine, _mobStateData, this),
            new MobBeingCarriedState(_stateMachine, _mobStateData, this)
        };

        _stateMachine.Initialize(states);
    }

    public void ResetMob()
    {
        _mobStateData.ResetData();
        _stateMachine.SwitchState<MobWalkingState>();
    }

    public void SetDestanation(Vector3 destanationPoint)
    {
        _mobStateData.Destination = destanationPoint;
    }

    public void Stop()
    {
        _agent.enabled = false;
    }

    public void Interact(IInteractor interactor)
    {
        _stateMachine.InputAction(interactor);
    }

    public void SetNewHolder(MobHolder holder)
    {
        if(_mobStateData.CurrentHolder != null)
        {
            Stolen?.Invoke();
            _mobStateData.CurrentHolder.ClearMob();
        }
        _mobStateData.CurrentHolder = holder;
        _mobStateData.Owner = holder.Owner;
    }

    public void Steal(IInteractor interactor)
    {
        _mobStateData.StealerPlayer = interactor;
        Taked?.Invoke();
    }

    public void Drop()
    {
        _mobStateData.StealerPlayer = null;
        transform.SetParent(null);
    }

    public void SetOnHolder()
    {
        transform.position = _mobStateData.CurrentHolder.HoldingPosition.position;
        transform.rotation = _mobStateData.CurrentHolder.HoldingPosition.rotation;
    }
}

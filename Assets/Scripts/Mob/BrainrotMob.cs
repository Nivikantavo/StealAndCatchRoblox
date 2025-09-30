using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BrainrotMob : MonoBehaviour, IInteractable
{
    public BrainrotMobConfig Config => _config;
    public GameObject Model => _config.MobPrefab;
    public float Speed => _agent.speed;
    public Transform SelfTransform => transform;
    
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

        List<IState> states = new List<IState>()
        {
            new MobWalkingState(_stateMachine, _agent, _mobStateData, this),
            new MobWorkingState(_stateMachine, _mobStateData, this),
            new MobBeingCarriedState(_stateMachine, _mobStateData)
        };

        _stateMachine = new StateMachine(states);
    }

    public void SetDestanation(Vector3 destanationPoint)
    {
        _mobStateData.Destination = destanationPoint;
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _agent.enabled = false;
    }

    public void Interact(IInteractor interactor)
    {
        _stateMachine.InputAction(interactor);
    }
}

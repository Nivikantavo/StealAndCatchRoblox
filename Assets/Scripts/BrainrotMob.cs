using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BrainrotMob : MonoBehaviour, IInteractable
{
    public BrainrotMobConfig Config => _config;
    public GameObject Model => _config.MobPrefab;
    public float Speed => _agent.speed;
    public Transform SelfTransform => transform;

    public InteractAction InteractAction { get; private set; }

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private MobInfoCanvas _mobInfoCanvas;

    private BrainrotMobConfig _config;

    public void Initialize(BrainrotMobConfig config)
    {
        _config = config;
        Instantiate(_config.MobPrefab, transform);
        _mobInfoCanvas.Initialize(_config.Name, _config.BaseCost.ToString(), false);
        InteractAction = new PurchasableMob(this, InteractActionType.Buy);
    }

    public void GoTo(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public void Interact(IInteractor interactor)
    {
        InteractAction.ExecuteAction(interactor);
    }
}

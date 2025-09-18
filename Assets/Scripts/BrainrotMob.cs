using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BrainrotMob : MonoBehaviour
{
    public BrainrotMobConfig Config => _config;
    public GameObject Model => _config.MobPrefab;
    public float Speed => _agent.speed;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private MobInfoCanvas _mobInfoCanvas;
    [SerializeField] private InteractableView _interactableView;
    private BrainrotMobConfig _config;
    private IInteractable _interactable;

    public void Initialize(BrainrotMobConfig config)
    {
        _config = config;
        Instantiate(_config.MobPrefab, transform);
        _interactable = new SellingMob(this);
        _mobInfoCanvas.Initialize(_config.Name, _config.BaseCost.ToString(), false);
        _interactableView.Initialize(_interactable);
    }

    public void GoTo(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

}

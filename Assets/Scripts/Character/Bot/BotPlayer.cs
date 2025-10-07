using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotPlayer : Player
{
    [SerializeField] private float _findTargetDistance;
    [SerializeField] private NavMeshAgent _agent;
    private BotInteractor _botInteractor => Interactor as BotInteractor;
    private IInteractable _currentTarget;

    private void Update()
    {
        if (_currentTarget == null)
        {
            ChooseTarget();
        }
        else if (_currentTarget != null && CanInteract() == false)
        {
            _agent.SetDestination(_currentTarget.SelfTransform.position);
        }
        else if (_currentTarget != null && CanInteract())
        {
            _currentTarget.Interact(_interactor);
        }
    }

    private void ChooseTarget()
    {
        List<IInteractable> interactables = _botInteractor.FindClosestInteractables(_findTargetDistance);

        foreach (var interactable in interactables)
        {
            if (interactable.Price <= Wallet.Money)
            {
                _currentTarget = interactable;
                break;
            }
        }
    }

    private bool CanInteract()
    {
        return Vector3.Distance(transform.position, _currentTarget.SelfTransform.position) < _botInteractor.InteractionRange;
    }
}

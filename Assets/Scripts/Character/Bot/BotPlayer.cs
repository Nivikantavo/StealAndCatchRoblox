using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BotPlayer : Player
{
    [SerializeField] private float _findTargetDistance;
    [SerializeField] private NavMeshAgent _agent;
    private BotInteractor _botInteractor => Interactor as BotInteractor;
    private IInteractable _currentTarget;

    private async void Update()
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
            _currentTarget = null;
            await UniTask.WaitForSeconds(2);
        }
    }

    private void ChooseTarget()
    {
        List<IInteractable> interactables = _botInteractor.FindClosestInteractables(_findTargetDistance);

        Vector3 myPosition = transform.position;
        var canBuy = interactables.Where(x => x.Price < Wallet.Money);

        if (!canBuy.Any())
        {
            _currentTarget = null;
            return;
        }

        int maxPrice = canBuy.Max(x => x.Price);
        _currentTarget = canBuy
            .Where(x => x.Price == maxPrice)
            .OrderBy(x => Vector3.Distance(myPosition, x.SelfTransform.position))
            .FirstOrDefault();
        
    }

    private bool CanInteract()
    {
        return Vector3.Distance(transform.position, _currentTarget.SelfTransform.position) < _botInteractor.InteractionRange;
    }
}

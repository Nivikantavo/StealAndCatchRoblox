using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BotPlayer : Player
{
    public Transform LockHouseButton => _botsHouse.LockButtonPosition;
    public bool EarnedEnough => _botsHouse.IsEarnedaLot();
    public float InteractionRange => _botInteractor.InteractionRange;
    public IInteractable StolenMob {  get; private set; }

    [SerializeField] private float _findTargetDistance;
    [SerializeField] private NavMeshAgent _agent;
    private BotInteractor _botInteractor => Interactor as BotInteractor;
    private BotsHouse _botsHouse => _house as BotsHouse;
    public IInteractable CurrentTarget { get; private set; }

    public void ResetTarget()
    {
        CurrentTarget = null;
    }

    public void GoTo(Transform target)
    {
        _agent.SetDestination(target.position);
    }

    public void ChooseOwnedTarget()
    {
        List<IInteractable> interactables = _botInteractor.FindClosestInteractables(_findTargetDistance);

        var canSteal = interactables.Where(x => x.Owner != null && x.IsGraped == false);

        CurrentTarget = GetNearestExpensive(canSteal);
    }

    public void ChooseFreeTarget()
    {
        List<IInteractable> interactables = _botInteractor.FindClosestInteractables(_findTargetDistance);

        var canBuy = interactables.Where(x => x.Price <= Wallet.Money && x.Owner == null);

        CurrentTarget = GetNearestExpensive(canBuy);
    }

    private IInteractable GetNearestExpensive(IEnumerable<IInteractable> interactables)
    {
        if (!interactables.Any())
        {
            CurrentTarget = null;
            return null;
        }

        int maxPrice = interactables.Max(x => x.Price);
        return interactables
            .Where(x => x.Price == maxPrice)
            .OrderBy(x => Vector3.Distance(transform.position, x.SelfTransform.position))
            .FirstOrDefault();
    }

    private bool CanInteract()
    {
        return Vector3.Distance(transform.position, CurrentTarget.SelfTransform.position) < _botInteractor.InteractionRange;
    }

    public override void OnMobStolen(IInteractable stolenMob)
    {
        StolenMob = stolenMob;
        CurrentTarget = StolenMob;
    }

    public override void OnMobLost(IInteractable stolenMob)
    {
        if(StolenMob == stolenMob)
        {
            StolenMob = null;
            CurrentTarget = null;
        }
    }
}

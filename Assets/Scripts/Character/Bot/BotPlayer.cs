using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BotPlayer : Player
{
    public BehaviorTreeData BehaviorTreeData { get; private set; }

    [SerializeField] private float _findTargetDistance;//вынести в конфиг
    [SerializeField] private NavMeshAgent _agent;

    private BotInteractor _botInteractor => Interactor as BotInteractor;
    private BotsHouse _botsHouse => _house as BotsHouse;

    protected override void Update()
    {
        base.Update();
        Debug.Log(BehaviorTreeData.Stealer);
    }

    public override void Initialize(House house)
    {
        base.Initialize(house);
        BehaviorTreeData = new BehaviorTreeData(_botsHouse, _botInteractor.InteractionRange, _botInteractor.InteractionRange);
    }

    public override void Attack()
    {
        if (AttackElapsedTime >= AttackCooldown)
        {
            _characterAnimation.SetAttack();
            var hittenPlayers = _fighter.Attack();
            foreach (var player in hittenPlayers)
            {
                if(player == BehaviorTreeData.Stealer)
                {
                    Debug.Log("hitten == Stealer");
                    OnMobLost(BehaviorTreeData.Stolen);
                }
            }
            AttackElapsedTime = 0;
        }
    }

    public void ResetTarget()
    {
        BehaviorTreeData.CurrentTarget = null;
    }

    public void GoTo(Transform target)
    {
        _agent.SetDestination(target.position);
    }

    public void ChooseOwnedTarget()
    {
        List<IInteractable> interactables = _botInteractor.FindClosestInteractables(_findTargetDistance);

        var canSteal = interactables.Where(x => x.Owner != null && x.Stealer == null);

        BehaviorTreeData.CurrentTarget = GetNearestExpensive(canSteal);
    }

    public void ChooseFreeTarget()
    {
        List<IInteractable> interactables = _botInteractor.FindClosestInteractables(_findTargetDistance);

        var canBuy = interactables.Where(x => x.Price <= Wallet.Money && x.Owner == null);

        BehaviorTreeData.CurrentTarget = GetNearestExpensive(canBuy);
    }

    private IInteractable GetNearestExpensive(IEnumerable<IInteractable> interactables)
    {
        if (!interactables.Any())
        {
            BehaviorTreeData.CurrentTarget = null;
            return null;
        }

        int maxPrice = interactables.Max(x => x.Price);
        return interactables
            .Where(x => x.Price == maxPrice)
            .OrderBy(x => Vector3.Distance(transform.position, x.SelfTransform.position))
            .FirstOrDefault();
    }

    public override void OnMobStolen(IInteractable stolenMob)
    {
        BehaviorTreeData.Stolen = stolenMob;
        BehaviorTreeData.Stealer = stolenMob.Stealer;
    }

    public override void OnMobLost(IInteractable stolenMob)
    {
        if(BehaviorTreeData.Stealer == stolenMob.Stealer)
        {
            BehaviorTreeData.Stolen = null;
            BehaviorTreeData.Stealer = null;
        }
    }
}

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
    private BotCharacterController _botCharacterController;

    public override void Initialize(House house)
    {
        base.Initialize(house);
        _botCharacterController = GetComponent<BotCharacterController>();
        BehaviorTreeData = new BehaviorTreeData(_botsHouse, _botInteractor.InteractionRange, _botInteractor.InteractionRange, _botCharacterController);
        
    }

    public override void Attack()
    {
        if (AttackElapsedTime >= AttackCooldown)
        {
            _characterAnimation.SetAttack();
            var hittenPlayers = _fighter.CheckAttackZone();
            foreach (var player in hittenPlayers)
            {
                if (ReferenceEquals((player as MonoBehaviour).gameObject, (BehaviorTreeData.Stealer as MonoBehaviour).gameObject))
                {
                    OnMobLost(BehaviorTreeData.Stolen);
                }
            }
            _fighter.Attack();
            AttackElapsedTime = 0;
        }
    }

    public void ResetTarget()
    {
        BehaviorTreeData.CurrentTarget = null;
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

    public void FindTargetToSteal()
    {
        var openHouse = FindOpenHouses(_findTargetDistance);
        if(openHouse == null)
        {
            throw new System.Exception("hasn't open house in range");
        }

        BehaviorTreeData.CurrentStealTargetHouse = openHouse;

        Collider[] hitColliders = Physics.OverlapSphere(openHouse.transform.position, _findTargetDistance);
        List<IInteractable> colsestInteractable = new List<IInteractable>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<IInteractable>(out var interactable))
            {
                if (colsestInteractable.Contains(interactable) == false && interactable.Owner != this)
                {
                    colsestInteractable.Add(interactable);
                }
            }
        }
        BehaviorTreeData.CurrentTarget = GetNearestExpensive(colsestInteractable);
    }

    public House FindOpenHouses(float findDistance)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, findDistance);
        House closestHouse = null;
        float minDistance = float.MaxValue;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<House>(out var house))
            {
                if (house.IsClosed == false && house.HasMobs)
                {
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestHouse = house;
                    }
                }
            }
        }
        return closestHouse;
    }
}

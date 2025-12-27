using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BotPlayer : Player
{
    public BehaviorTreeData BehaviorTreeData { get; private set; }
    [SerializeField] private BehaviorTree _behaviorTree;
    [SerializeField] private TextMeshProUGUI _playerNameView;
    //вынести в конфиг
    [SerializeField] private float _findTargetDistance;
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _carringSpeed;

    private BotInteractor _botInteractor => Interactor as BotInteractor;
    private BotsHouse _botsHouse => _house as BotsHouse;
    private BotCharacterController _botCharacterController;

    public override void Initialize(House house, string name, IPersistenData data = null)
    {
        base.Initialize(house, name);

        if (data == null)
        {
            data = new PersistenData();
            data.UserData = new UserData();// TODO: убрать костыль
        }
        _wallet = new InGameWallet(data);

        _botCharacterController = GetComponent<BotCharacterController>();
        BehaviorTreeData = new BehaviorTreeData(_botsHouse, _botInteractor.InteractionRange, _botInteractor.InteractionRange, _botCharacterController);
        _botCharacterController.agent.avoidancePriority = gameObject.layer;
        _behaviorTree.DisableBehavior();
        _behaviorTree.EnableBehavior();
        _playerNameView.text = name;
    }

    public override void Attack()
    {
        if (AttackElapsedTime >= AttackCooldown)
        {
            _characterAnimation.SetAttack();
            var hittenPlayers = _fighter.CheckAttackZone();
            foreach (var player in hittenPlayers)
            {
                if (ReferenceEquals((player as MonoBehaviour).gameObject, (BehaviorTreeData.StealerFromMe as MonoBehaviour).gameObject))
                {
                    OnMobLost(BehaviorTreeData.StolenFromMe);
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

        var canBuy = interactables.Where(x => x.Price <= Wallet.Balance && x.Owner == null);

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
        BehaviorTreeData.StolenFromMe = stolenMob;
        BehaviorTreeData.StealerFromMe = stolenMob.Stealer;
    }

    public override void OnMobLost(IInteractable stolenMob)
    {
        if(BehaviorTreeData.StealerFromMe == stolenMob.Stealer)
        {
            BehaviorTreeData.StolenFromMe = null;
            BehaviorTreeData.StealerFromMe = null;
        }
    }

    public override void TakeKnokout()
    {
        _botCharacterController.pause = true;
        StartCoroutine(Stunned());
    }

    private IEnumerator Stunned()
    {
        yield return new WaitForSeconds(5);
        _botCharacterController.pause = false;
        _characterAnimation.SetIsKnoked(false);
    }

    public void FindTargetToSteal()
    {
        var openHouse = FindOpenHouses(_findTargetDistance * 5);
        if(openHouse == null)
        {
            return;
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

    public override void OnMobTaken()
    {
        _botCharacterController.speed = _carringSpeed;
    }

    public override void OnMobReleased()
    {
        _botCharacterController.speed = _defaultSpeed;
    }
}

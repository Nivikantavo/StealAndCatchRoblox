using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeData
{
    public Func<bool> AvailabilityCondition { get; set; }
    public bool IsTargetAvailable
    {
        get
        {
            if (AvailabilityCondition != null)
                return AvailabilityCondition();

            return true;
        }
    }

    public Transform TargetPosition { get; set; }
    public float CurrentRange { get; set; }
    public float InteractionRange { get; private set; }
    public float AttackRange { get; private set; }
    public IInteractor StealerFromMe { get; set; }
    public IInteractable CurrentTarget { get; set; }
    public House CurrentStealTargetHouse { get; set; }
    public IInteractable StolenFromMe { get; set; }
    public IEnumerable<Transform> FullCollecters => _botsHouse.FullCollecters;
    public BotCharacterController BotCharacterController => _botCharacterController;
    public Transform LockHouseButton => _botsHouse.LockButtonPosition;
    public bool EarnedEnough => _botsHouse.IsEarnedaLot();
    public bool IsHouseClosed => _botsHouse.IsClosed;
    public bool HasMobsOnHouse => _botsHouse.HasMobs;
    public bool HasFreeHolder => _botsHouse.HasFreeHolder;

    private BotsHouse _botsHouse;
    private BotCharacterController _botCharacterController;

    public BehaviorTreeData(BotsHouse botsHouse, float interactionRange, float attackRange, BotCharacterController controller)
    {
        _botsHouse = botsHouse;
        _botCharacterController = controller;
        InteractionRange = interactionRange;
        AttackRange = attackRange;
    }

    public IInteractable GetCheapestMobInHouse()
    {
        return _botsHouse.CheapestMob;
    }
}

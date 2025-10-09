using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeData
{
    public IInteractor Stealer { get; set; }
    public IInteractable CurrentTarget { get; set; }
    public IInteractable Stolen { get; set; }
    public Transform LockHouseButton => _botsHouse.LockButtonPosition;
    public bool EarnedEnough => _botsHouse.IsEarnedaLot();
    public bool IsHouseClosed => _botsHouse.IsClosed;
    public bool HasMobsOnHouse => _botsHouse.HasMobs;
    public float InteractionRange { get; private set; }
    public float AttackRange { get; private set; }

    private BotsHouse _botsHouse;

    public BehaviorTreeData(BotsHouse botsHouse, float interactionRange, float attackRange)
    {
        _botsHouse = botsHouse;
        InteractionRange = interactionRange;
        AttackRange = attackRange;
    }
}

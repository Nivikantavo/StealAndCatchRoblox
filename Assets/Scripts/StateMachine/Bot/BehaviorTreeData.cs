using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeData
{
    public Transform TargetPosition { get; set; }
    public float CurrentRange { get; set; }

    public IInteractor Stealer { get; set; }
    public IInteractable CurrentTarget { get; set; }
    public IInteractable Stolen { get; set; }
    public IEnumerable<Transform> FullCollecters => _botsHouse.FullCollecters;
    public BotCharacterController BotCharacterController => _botCharacterController;
    public Transform LockHouseButton => _botsHouse.LockButtonPosition;
    public bool EarnedEnough => _botsHouse.IsEarnedaLot();
    public bool IsHouseClosed => _botsHouse.IsClosed;
    public bool HasMobsOnHouse => _botsHouse.HasMobs;
    public float InteractionRange { get; private set; }
    public float AttackRange { get; private set; }
    

    private BotsHouse _botsHouse;
    private BotCharacterController _botCharacterController;

    public BehaviorTreeData(BotsHouse botsHouse, float interactionRange, float attackRange, BotCharacterController controller)
    {
        _botsHouse = botsHouse;
        _botCharacterController = controller;
        InteractionRange = interactionRange;
        AttackRange = attackRange;
    }
}

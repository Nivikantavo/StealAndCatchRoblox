using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BotsHouse : House
{
    public Transform OwnerSpawnPosition => _ownerSpawnPosition;

    [SerializeField] private Transform _ownerSpawnPosition;

    public void Initialzie(BotPlayer owner, int layer)
    {
        Owner = owner;
        Owner.Initialize(this);
        Owner.gameObject.layer = LayerNumber;
        MobCatcher.Initialize(Owner);
        Locker.Initialize(Owner);
    }
}

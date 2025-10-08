using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BotsHouse : House
{
    public Transform OwnerSpawnPosition => _ownerSpawnPosition;
    public Transform LockButtonPosition => Locker.LockButtonPosition;
    public bool HasMobs => Holders.Any(holder => holder.IsFree == false);

    [SerializeField] private Transform _ownerSpawnPosition;

    public void Initialzie(BotPlayer owner, int layer)
    {
        Owner = owner;
        Owner.Initialize(this);
        Owner.gameObject.layer = LayerNumber;
        MobCatcher.Initialize(Owner);
        Locker.Initialize(Owner);
        SecuritySystem.Initialize(Holders, Owner);
    }

    public bool IsEarnedaLot()
    {
        if(HasMobs == false)
            return false;

        if(Holders.Any(holder => holder.MaxValue == holder.Earned))
        {
            return true;
        }
        return false;
    }
}

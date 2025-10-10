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
    //public List<Transform> FullCollecters => Holders.Where(holder => holder.Earned == holder.MaxValue).Select(holder=> holder.CollectPosition).ToList();
    public IEnumerable<Transform> FullCollecters
    {
        get
        {
            foreach (var holder in Holders)
            {
                if (holder.Earned == holder.MaxValue)
                    yield return holder.CollectPosition;
            }
        }
    }

    [SerializeField] private Transform _ownerSpawnPosition;

    public void Initialzie(BotPlayer owner, int layer)
    {
        Owner = owner;
        Owner.Initialize(this);
        LayerNumber = layer;
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

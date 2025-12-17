using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BotsHouse : House
{
    
    public Transform LockButtonPosition => Locker.LockButtonPosition;
    //public List<Transform> FullCollecters => Holders.Where(holder => holder.Earned == holder.MaxValue).Select(holder=> holder.CollectPosition).ToList();
    public IEnumerable<Transform> FullCollecters
    {
        get
        {
            foreach (var holder in Holders)
            {
                if(holder.IsFree || holder.Earned == 0)
                    continue;
                if (holder.Earned == holder.MaxValue)
                    yield return holder.CollectPosition;
            }
        }
    }

    public IInteractable CheapestMob => Holders.FirstOrDefault(mob => mob.MaxValue == Holders.Max(holder => holder.MaxValue)).Mob;

    

    public void Initialzie(BotPlayer owner, int layer)
    {
        Owner = owner;
        LayerNumber = layer;
        Owner.gameObject.layer = LayerNumber;
        Owner.Initialize(this);
        MobCatcher.Initialize(Holders, Owner);
        Locker.Initialize(Owner);
        SecuritySystem.Initialize(Holders, Owner);
    }

    public bool IsEarnedaLot()//TODO: вывести в конфиг процент или рандомизировать его
    {
        if(HasMobs == false)
            return false;

        var nonFreeCount = Holders.Count(holder => !holder.IsFree);
        if (nonFreeCount > 0)
        {
            var maxValueCount = Holders.Count(holder => holder.MaxValue == holder.Earned);
            if (maxValueCount > nonFreeCount * 0.5f)
            {
                return true;
            }
        }
        return false;
    }
}

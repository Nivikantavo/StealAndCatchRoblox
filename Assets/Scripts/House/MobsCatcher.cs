using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MobsCatcher : MonoBehaviour
{
    public event Action<BrainrotMob> MobAdded;
    public bool HasFreeHolder => _mobHolders.Exists(holder => holder.IsFree);
    public bool HasMobs => _mobHolders.Exists(holder => holder.IsFree == false);

    private List<MobHolder> _mobHolders;

    public void Initialize(List<MobHolder> holders, Player owner)
    {
        _mobHolders = holders;
        foreach (var mobHolder in _mobHolders)
        {
            mobHolder.Initialize(owner);
        }
    }

    public MobHolder GetFreeHolder()
    {
        return _mobHolders.FirstOrDefault(holder => holder.IsFree);
    }

    public void Restart()
    {
        foreach (var mobHolder in _mobHolders)
        {
            mobHolder.Restart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BrainrotMob mob))
        {
            if (HasFreeHolder)
            {
                SetMobOnHolder(mob);
            }
        }
    }

    private void SetMobOnHolder(BrainrotMob mob)
    {
        MobHolder holder = _mobHolders.FirstOrDefault(holder => holder.ItsMyMob(mob));
        if(holder == null)
        {
            holder = _mobHolders.FirstOrDefault(holder => holder.IsFree);
            if (holder != null)
            {
                mob.SetNewHolder(holder);
                mob.Drop();
                holder.SetMob(mob);
            }
            else
            {
                return;
            }
        }
        MobAdded?.Invoke(mob);
    }
}

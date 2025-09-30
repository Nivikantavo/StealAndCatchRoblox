using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MobsCatcher : MonoBehaviour
{
    public bool HasFreeHolder => _mobHolders.Exists(holder => holder.IsFree);

    [SerializeField] private List<MobHolder> _mobHolders;

    public void Initialize(Player owner)
    {
        foreach (var mobHolder in _mobHolders)
        {
            mobHolder.Initialize(owner);
        }
    }

    public MobHolder GetFreeHolder()
    {
        return _mobHolders.FirstOrDefault(holder => holder.IsFree);
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
        if (holder != null)
        {
            holder.SetMobOnPosition();
        }
    }
}

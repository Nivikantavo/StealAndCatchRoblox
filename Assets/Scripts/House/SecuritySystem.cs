using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuritySystem : MonoBehaviour
{
    private List<MobHolder> _mobHolders;
    private Player _player;

    public void Initialize(List<MobHolder> holders, Player player)
    {
        _mobHolders = holders;
        _player = player;
        foreach (MobHolder holder in _mobHolders)
        {
            holder.MobWasStolen += OnMobStolen;
            holder.StolenMobLost += OnMobLost;
        }
    }

    private void OnDisable()
    {
        foreach (MobHolder holder in _mobHolders)
        {
            holder.MobWasStolen -= OnMobStolen;
            holder.StolenMobLost -= OnMobLost;
        }
    }

    private void OnMobStolen(IInteractable stolenMob)
    {
        _player.OnMobStolen(stolenMob);
    }

    private void OnMobLost(IInteractable stolenMob)
    {
        _player.OnMobLost(stolenMob);
    }
}

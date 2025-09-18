using System;
using UnityEngine;

public class SellingMob : IInteractable
{
    public Action MobWasSold;

    private BrainrotMob _mob;

    public SellingMob(BrainrotMob mob)
    {
        _mob = mob;
    }

    public void Interact(Transform targetHouse)
    {
        Sell(targetHouse);
    }

    private void Sell(Transform targetHouse)
    {
        _mob.GoTo(targetHouse.position);
        MobWasSold?.Invoke();
    }
}

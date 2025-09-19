using System;
using UnityEngine;

public class PurchasableMob : InteractAction
{
    private BrainrotMob _mob;
    public PurchasableMob(BrainrotMob mob, InteractActionType actionType) : base(actionType)
    {
        _mob = mob;
    }

    public override void ExecuteAction(IInteractor interactor)
    {
        Sell(interactor.HouseTransform);
    }

    public void Sell(Transform targetHouse)
    {
        _mob.GoTo(targetHouse.position);
    }
}

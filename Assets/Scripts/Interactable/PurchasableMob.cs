using System;
using UnityEngine;

public class PurchasableMob : InteractAction
{
    private BrainrotMob _mob;
    public PurchasableMob(BrainrotMob mob)
    {
        _mob = mob;
    }

    public override bool TryExecuteAction(IInteractor interactor)
    {
        MobHolder holder = interactor.MobHolder;
        if (holder == null)
        {
            return false;
        }
        else if (interactor.Wallet.IsEnough(_mob.Config.BaseCost) == false)
        {
            return false;
        }
        else
        {
            interactor.Wallet.Spend(_mob.Config.BaseCost);
            ExecuteAction(interactor);
            PurchaseMob(holder);
            holder.SetMob(_mob);
            return true;
        }
    }

    protected override void ExecuteAction(IInteractor interactor)
    {
        base.ExecuteAction(interactor);
    }

    private void PurchaseMob(MobHolder holder)
    {
        _mob.SetNewHolder(holder);
    }
}

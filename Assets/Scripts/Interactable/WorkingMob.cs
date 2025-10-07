using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingMob : InteractAction
{
    public event Action<BrainrotMob> OnMobSold;

    private IInteractor _owner;
    private BrainrotMob _mob;

    public WorkingMob(IInteractor owner, BrainrotMob mob)
    {
        _owner = owner;
        _mob = mob;
    }

    public override bool TryExecuteAction(IInteractor interactor)
    {
        if(interactor.SelfTransform == _owner.SelfTransform)
        {
            SellMob();
        }
        else
        {
            StealMob(interactor);
        }
        return true;
    }

    private void SellMob()
    {
        _owner.Wallet.AddMoney(_mob.Config.BaseCost);
        OnMobSold?.Invoke(_mob);
        _mob.gameObject.SetActive(false);
    }

    private void StealMob(IInteractor interactor)
    {
        _mob.Steal(interactor);
    }
}

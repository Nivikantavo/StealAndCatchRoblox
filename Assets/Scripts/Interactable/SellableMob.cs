using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellableMob : InteractAction
{
    private IInteractor _owner => _mobStateData.Owner;

    private MobStateData _mobStateData;
    private BrainrotMob _mob;

    public SellableMob(MobStateData data, BrainrotMob mob)
    {
        _mobStateData = data;
        _mob = mob;
    }

    public override bool TryExecuteAction(IInteractor interactor)
    {
        if (interactor.SelfTransform == _owner.SelfTransform)
        {
            SellMob();
        }
        return true;
    }

    private void SellMob()
    {
        _owner.Wallet.AddMoney(_mob.Config.BaseCost);
        _mob.gameObject.SetActive(false);
    }
}

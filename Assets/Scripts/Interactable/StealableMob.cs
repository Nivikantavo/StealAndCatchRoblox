using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealableMob : InteractAction
{
    private IInteractor _owner => _mobStateData.Owner;

    private MobStateData _mobStateData;
    private BrainrotMob _mob;

    public StealableMob(MobStateData data, BrainrotMob mob)
    {
        _mobStateData = data;
        _mob = mob;
    }

    public override bool TryExecuteAction(IInteractor interactor)
    {
        if (interactor.SelfTransform != _owner.SelfTransform)
        {
            StealMob(interactor);
        }
        return true;
    }

    private void StealMob(IInteractor interactor)
    {
        _mob.Steal(interactor);
    }
}

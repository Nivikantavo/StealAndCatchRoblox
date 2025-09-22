using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingMob : InteractAction
{
    private Player _owner;
    private BrainrotMob _mob;
    private MobHolder _mobHolder;

    public WorkingMob(Player owner, BrainrotMob mob, MobHolder holder)
    {
        _owner = owner;
        _mob = mob;
        _mobHolder = holder;
    }

    public override bool TryExecuteAction(IInteractor interactor)
    {
        if(interactor.Wallet == _owner.Wallet)
        {
            SellMob();
        }
        else
        {
            StealMob();
        }
        return true;
    }

    private void SellMob()
    {
        _owner.Wallet.AddMoney(_mob.Config.BaseCost);
        _mobHolder.ClearMob();
        _mob.gameObject.SetActive(false);
    }

    private void StealMob()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHouse : House
{
    public event Action<BrainrotMob> MobAdded;

    [Inject]
    private void Construct(UserPlayer owner)
    {
        Owner = owner;
        Owner.Initialize(this, string.Empty);
        LayerNumber = 7;
        Owner.gameObject.layer = LayerNumber;
        MobCatcher.Initialize(Holders, Owner);
        Locker.Initialize(Owner);
        SecuritySystem.Initialize(Holders, Owner);

        MobCatcher.MobAdded += OnMobAdded;
    }

    private void OnDisable() //TODO: заменить на OnDispose
    {
        MobCatcher.MobAdded -= OnMobAdded;
    }

    private void OnMobAdded(BrainrotMob mob)
    {
        MobAdded?.Invoke(mob);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHouse : House
{
    [Inject]
    private void Construct(UserPlayer owner)
    {
        Owner = owner;
        Owner.Initialize(this);
        LayerNumber = 7;
        Owner.gameObject.layer = LayerNumber;
        MobCatcher.Initialize(Owner);
        Locker.Initialize(Owner);
        SecuritySystem.Initialize(Holders, Owner);
    }
}

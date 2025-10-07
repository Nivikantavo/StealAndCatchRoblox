using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStealer
{
    public bool IsCarries { get; }

    void GrabMob(BrainrotMob mob);
    void LoseMob();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStealer
{
    event Action MobWasTaken;
    event Action MobWasReleased;
    public bool IsCarries { get; }
    void GrabMob(BrainrotMob mob);
    void LoseMob();
}

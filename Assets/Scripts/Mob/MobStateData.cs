using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStateData
{
    public Vector3 Destination { get; set; }
    public bool IsSold { get; set; }
    public MobHolder CurrentHolder { get; set; }
    public IInteractor Owner { get; set; }
    public IInteractor StealerPlayer { get; set; }

    public void ResetData()
    {
        Destination = Vector3.zero;
        IsSold = false;
        Owner = null;
        StealerPlayer = null;
        CurrentHolder = null;
    }
}

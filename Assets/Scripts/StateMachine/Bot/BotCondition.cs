using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCondition : Conditional
{
    protected BotPlayer Bot;

    public override void OnAwake()
    {
        Bot = GetComponent<BotPlayer>();
    }
}

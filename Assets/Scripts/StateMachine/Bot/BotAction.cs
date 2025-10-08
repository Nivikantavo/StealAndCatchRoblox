using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAction : Action
{
    protected BotPlayer Bot;

    public override void OnAwake()
    {
        Bot = GetComponent<BotPlayer>();
    }
}

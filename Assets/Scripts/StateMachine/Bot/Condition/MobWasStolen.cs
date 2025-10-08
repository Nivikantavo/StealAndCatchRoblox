using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobWasStolen : BotCondition
{
    public override TaskStatus OnUpdate()
    {
        return Bot.StolenMob == null ? TaskStatus.Success : TaskStatus.Failure;
    }
}

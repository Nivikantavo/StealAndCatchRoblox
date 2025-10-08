using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedLockHouseCondition : BotCondition
{
    public override TaskStatus OnUpdate()
    {
        return Bot.IsHouseClosed ? TaskStatus.Failure : TaskStatus.Success;
    }
}

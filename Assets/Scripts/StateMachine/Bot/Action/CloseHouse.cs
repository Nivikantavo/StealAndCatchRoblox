using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHouse : BotAction
{
    public override void OnStart()
    {
        Bot.GoTo(Bot.LockHouseButton);
    }

    public override TaskStatus OnUpdate()
    {
        return Bot.IsHouseClosed ? TaskStatus.Success : TaskStatus.Running;
    }
}

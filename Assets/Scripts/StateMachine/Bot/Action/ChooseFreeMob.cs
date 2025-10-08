using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseFreeMob : BotAction
{
    public override void OnStart()
    {
        Bot.ChooseFreeTarget();
    }

    public override TaskStatus OnUpdate()
    {
        if(Bot.CurrentTarget == null)
        {
            return TaskStatus.Failure;
        }

        if(Bot.CurrentTarget.Owner != null)
        {
            return TaskStatus.Failure;
        }

        return TaskStatus.Success;
    }
}

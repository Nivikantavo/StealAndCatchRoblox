using BehaviorDesigner.Runtime.Tasks;
using BotBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealMob : BotAction
{
    public override void OnStart()
    {
        Bot.BehaviorTreeData.CurrentTarget.Interact(Bot.Interactor);
    }

    public override TaskStatus OnUpdate()
    {
        if (Bot.BehaviorTreeData.CurrentTarget.Stealer == Bot.Interactor)
        {
            Bot.BehaviorTreeData.AvailabilityCondition = null;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

using BehaviorDesigner.Runtime.Tasks;
using BotBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMobForSteal : BotAction
{
    public override void OnStart()
    {
        Bot.FindTargetToSteal();
        if (Bot.BehaviorTreeData.CurrentTarget != null && Bot.BehaviorTreeData.CurrentTarget.Stealer == null)
        {
            Bot.BehaviorTreeData.TargetPosition = Bot.BehaviorTreeData.CurrentTarget.SelfTransform;
            Bot.BehaviorTreeData.CurrentRange = Bot.BehaviorTreeData.InteractionRange;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (Bot.BehaviorTreeData.CurrentTarget == null)
        {
            return TaskStatus.Failure;
        }

        if (Bot.BehaviorTreeData.CurrentTarget.Owner == null || Bot.BehaviorTreeData.CurrentTarget.Stealer != null)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}

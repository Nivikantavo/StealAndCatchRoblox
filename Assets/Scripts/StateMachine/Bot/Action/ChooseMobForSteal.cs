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

        Bot.BehaviorTreeData.AvailabilityCondition = () =>
        {
            if (Bot.BehaviorTreeData.CurrentTarget == null)
            {
                return false;
            }

            if (Bot.BehaviorTreeData.CurrentTarget.Owner == null)
            {
                return false;
            }

            if (Bot.BehaviorTreeData.CurrentTarget.Stealer != null)
            {
                if (Bot.BehaviorTreeData.CurrentTarget.Stealer != Bot.Interactor)
                {
                    return false;
                }
            }
            if (Bot.BehaviorTreeData.CurrentStealTargetHouse.IsClosed)
            {
                return false;
            }

            return true;
        };

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

        if (Bot.BehaviorTreeData.CurrentTarget.Owner == null)
        {
            return TaskStatus.Failure;
        }

        if (Bot.BehaviorTreeData.CurrentTarget.Stealer != null)
        {
            if(Bot.BehaviorTreeData.CurrentTarget.Stealer != Bot.Interactor)
            {
                return TaskStatus.Failure;
            }
        }
        return TaskStatus.Success;
    }
}

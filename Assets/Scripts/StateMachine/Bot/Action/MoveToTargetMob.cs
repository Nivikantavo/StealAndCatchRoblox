using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetMob : BotAction
{
    public override TaskStatus OnUpdate()
    {
        if(Bot.CurrentTarget == null)
        {
            return TaskStatus.Failure;
        }
        if(Bot.CurrentTarget.Owner != null && Bot.CurrentTarget.Owner != Bot)
        {
            return TaskStatus.Failure;
        }
        if (Vector3.Distance(Bot.CurrentTarget.SelfTransform.position, Bot.transform.position) < Bot.InteractionRange)
        {
            return TaskStatus.Success;
        }
        Bot.GoTo(Bot.CurrentTarget.SelfTransform);
        
        return TaskStatus.Running;
    }
}

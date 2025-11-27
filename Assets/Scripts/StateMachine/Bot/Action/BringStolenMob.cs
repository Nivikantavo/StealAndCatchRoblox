using BehaviorDesigner.Runtime.Tasks;
using BotBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringStolenMob : BotAction
{
    public override void OnStart()
    {
        Bot.BehaviorTreeData.BotCharacterController.GoTo(Bot.BehaviorTreeData.LockHouseButton.position);
    }

    public override TaskStatus OnUpdate()
    {
        if(Bot.Stealer.IsCarries == false)
        {
            return TaskStatus.Failure;
        }

        if (Bot.BehaviorTreeData.CurrentTarget.Owner == Bot.Interactor)
        {
            Bot.ResetTarget();
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
        
    }
}

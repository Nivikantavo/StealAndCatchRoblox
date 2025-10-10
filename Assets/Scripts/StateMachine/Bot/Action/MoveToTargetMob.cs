using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BotBehavior
{
    public class MoveToTargetMob : BotAction
    {
        public override TaskStatus OnUpdate()
        {
            if (Bot.BehaviorTreeData.CurrentTarget == null)
            {
                return TaskStatus.Failure;
            }
            if (Bot.BehaviorTreeData.CurrentTarget.Owner != null)
            {
                return TaskStatus.Failure;
            }
            if (Vector3.Distance(Bot.BehaviorTreeData.CurrentTarget.SelfTransform.position, Bot.transform.position) < Bot.BehaviorTreeData.InteractionRange)
            {
                return TaskStatus.Success;
            }
            Bot.BehaviorTreeData.BotCharacterController.GoTo(Bot.BehaviorTreeData.CurrentTarget.SelfTransform);

            return TaskStatus.Running;
        }
    }
}

using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BotBehavior
{
    public class MoveToStealer : BotAction
    {
        public override TaskStatus OnUpdate()
        {
            if (Bot.BehaviorTreeData.Stealer == null)
            {
                return TaskStatus.Failure;
            }
            if (Vector3.Distance(Bot.BehaviorTreeData.Stealer.SelfTransform.position, Bot.transform.position) < Bot.BehaviorTreeData.AttackRange)
            {
                return TaskStatus.Success;
            }
            Bot.BehaviorTreeData.BotCharacterController.GoTo(Bot.BehaviorTreeData.Stealer.SelfTransform);

            return TaskStatus.Running;
        }
    }
}

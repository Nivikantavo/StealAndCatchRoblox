using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BotBehavior
{
    public class MoveToTarget : BotAction
    {
        public override TaskStatus OnUpdate()
        {
            if(Bot.BehaviorTreeData.IsTargetAvailable == false)
            {
                return TaskStatus.Failure;
            }

            if (Vector3.Distance(Bot.BehaviorTreeData.TargetPosition.position, Bot.transform.position) < Bot.BehaviorTreeData.CurrentRange)
            {
                return TaskStatus.Success;
            }
            Bot.BehaviorTreeData.BotCharacterController.GoTo(Bot.BehaviorTreeData.TargetPosition.position);

            return TaskStatus.Running;
        }
    }
}

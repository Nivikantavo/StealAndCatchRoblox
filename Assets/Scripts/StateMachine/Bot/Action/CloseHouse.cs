using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BotBehavior
{
    public class CloseHouse : BotAction
    {
        public override void OnStart()
        {
            Bot.BehaviorTreeData.BotCharacterController.GoTo(Bot.BehaviorTreeData.LockHouseButton);
        }

        public override TaskStatus OnUpdate()
        {
            return Bot.BehaviorTreeData.IsHouseClosed ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}

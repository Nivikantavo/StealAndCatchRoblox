using BehaviorDesigner.Runtime.Tasks;

namespace BotBehavior
{
    public class CloseHouse : BotAction
    {
        public override void OnStart()
        {
            Bot.GoTo(Bot.BehaviorTreeData.LockHouseButton);
        }

        public override TaskStatus OnUpdate()
        {
            return Bot.BehaviorTreeData.IsHouseClosed ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}

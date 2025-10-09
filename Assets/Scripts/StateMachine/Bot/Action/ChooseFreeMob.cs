using BehaviorDesigner.Runtime.Tasks;

namespace BotBehavior
{
    public class ChooseFreeMob : BotAction
    {
        public override void OnStart()
        {
            Bot.ChooseFreeTarget();
        }

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

            return TaskStatus.Success;
        }
    }
}

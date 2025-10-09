using BehaviorDesigner.Runtime.Tasks;

namespace BotBehavior
{
    public class BuyMob : BotAction
    {
        public override void OnStart()
        {
            Bot.BehaviorTreeData.CurrentTarget.Interact(Bot.Interactor);
        }

        public override TaskStatus OnUpdate()
        {
            if (Bot.BehaviorTreeData.CurrentTarget.Owner == Bot.Interactor)
            {
                Bot.ResetTarget();
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}

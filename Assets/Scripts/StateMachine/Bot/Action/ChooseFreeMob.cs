using BehaviorDesigner.Runtime.Tasks;

namespace BotBehavior
{
    public class ChooseFreeMob : BotAction
    {
        public override void OnStart()
        {
            Bot.ChooseFreeTarget();
            Bot.BehaviorTreeData.AvailabilityCondition = () =>
            {
                if (Bot.BehaviorTreeData.CurrentTarget != null)
                {
                    if(Bot.BehaviorTreeData.CurrentTarget.Owner != null)
                    {
                        return false;
                    }
                }
                return true;
                    
            };
            if (Bot.BehaviorTreeData.CurrentTarget != null && Bot.BehaviorTreeData.CurrentTarget.Owner == null)
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

            if (Bot.BehaviorTreeData.CurrentTarget.Owner != null)
            {
                return TaskStatus.Failure;
            }

            return TaskStatus.Success;
        }
    }
}

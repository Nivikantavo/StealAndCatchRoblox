using BehaviorDesigner.Runtime.Tasks;

namespace BotBehavior
{
    public class NeedLockHouseCondition : BotCondition
    {
        public override TaskStatus OnUpdate()
        {
            if (Bot.BehaviorTreeData.IsHouseClosed)
            {
                return TaskStatus.Failure;
            }
            else
            {
                if (Bot.BehaviorTreeData.HasMobsOnHouse)
                {
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }
    }
}

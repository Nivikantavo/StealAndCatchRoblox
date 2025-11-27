using BehaviorDesigner.Runtime.Tasks;
using BotBehavior;

public class HouseIsFull : BotCondition
{
    public override TaskStatus OnUpdate()
    {
        if (Bot.BehaviorTreeData.HasFreeHolder == false)
        {
            Bot.BehaviorTreeData.CurrentTarget = Bot.BehaviorTreeData.GetCheapestMobInHouse();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

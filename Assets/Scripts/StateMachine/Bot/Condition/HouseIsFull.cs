using BehaviorDesigner.Runtime.Tasks;
using BotBehavior;
using UnityEngine;

public class HouseIsFull : BotCondition
{
    public override TaskStatus OnUpdate()
    {
        if (Bot.BehaviorTreeData.HasFreeHolder == false)
        {
            Debug.Log("Нет свободных мест");
            Bot.BehaviorTreeData.CurrentTarget = Bot.BehaviorTreeData.GetCheapestMobInHouse();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

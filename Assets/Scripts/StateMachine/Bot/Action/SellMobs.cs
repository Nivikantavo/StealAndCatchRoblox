using BehaviorDesigner.Runtime.Tasks;
using BotBehavior;

public class SellMobs : BotAction
{
    public override void OnStart()
    {
        Bot.BehaviorTreeData.CurrentTarget.Interact(Bot.Interactor);
    }

    public override TaskStatus OnUpdate()
    {
        Bot.ResetTarget();
        return TaskStatus.Success;
    }
}

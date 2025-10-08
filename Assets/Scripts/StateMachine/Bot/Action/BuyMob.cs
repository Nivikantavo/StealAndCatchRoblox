using BehaviorDesigner.Runtime.Tasks;

public class BuyMob : BotAction
{
    public override void OnStart()
    {
        Bot.CurrentTarget.Interact(Bot.Interactor);
    }

    public override TaskStatus OnUpdate()
    {
        if(Bot.CurrentTarget.Owner == Bot.Interactor)
        {
            Bot.ResetTarget();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

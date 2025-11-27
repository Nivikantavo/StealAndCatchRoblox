using BehaviorDesigner.Runtime.Tasks;

namespace BotBehavior
{
    public class Attack : BotAction
    {
        public override void OnStart()
        {
            Bot.Attack();
        }

        public override TaskStatus OnUpdate()
        {
            return Bot.BehaviorTreeData.StealerFromMe == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}
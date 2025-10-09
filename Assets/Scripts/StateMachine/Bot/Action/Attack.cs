
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
            return Bot.BehaviorTreeData.Stealer == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}
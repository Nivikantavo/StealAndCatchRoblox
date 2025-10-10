using BehaviorDesigner.Runtime.Tasks;

namespace BotBehavior
{
    public class MobWasStolen : BotCondition
    {
        public override void OnStart()
        {
            if(Bot.BehaviorTreeData.Stealer != null)
            {
                Bot.BehaviorTreeData.TargetPosition = Bot.BehaviorTreeData.Stealer.SelfTransform;
                Bot.BehaviorTreeData.CurrentRange = Bot.BehaviorTreeData.AttackRange;
            }
        }

        public override TaskStatus OnUpdate()
        {
            return Bot.BehaviorTreeData.Stealer == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}

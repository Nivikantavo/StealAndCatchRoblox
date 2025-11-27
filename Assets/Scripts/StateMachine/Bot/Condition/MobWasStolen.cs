using BehaviorDesigner.Runtime.Tasks;

namespace BotBehavior
{
    public class MobWasStolen : BotCondition
    {
        public override void OnStart()
        {
            if(Bot.BehaviorTreeData.StealerFromMe != null)
            {
                Bot.BehaviorTreeData.TargetPosition = Bot.BehaviorTreeData.StealerFromMe.SelfTransform;
                Bot.BehaviorTreeData.CurrentRange = Bot.BehaviorTreeData.AttackRange;
            }
        }

        public override TaskStatus OnUpdate()
        {
            return Bot.BehaviorTreeData.StealerFromMe == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}

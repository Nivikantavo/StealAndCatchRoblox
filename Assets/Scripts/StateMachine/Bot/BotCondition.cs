using BehaviorDesigner.Runtime.Tasks;

namespace BotBehavior
{
    public class BotCondition : Conditional
    {
        protected BotPlayer Bot;

        public override void OnAwake()
        {
            Bot = GetComponent<BotPlayer>();
        }
    }
}

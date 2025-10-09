using BehaviorDesigner.Runtime.Tasks;

namespace BotBehavior
{
    public class BotAction : Action
    {
        protected BotPlayer Bot;

        public override void OnAwake()
        {
            Bot = GetComponent<BotPlayer>();
        }
    }
}

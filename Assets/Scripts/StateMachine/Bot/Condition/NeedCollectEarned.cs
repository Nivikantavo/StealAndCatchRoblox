using BehaviorDesigner.Runtime.Tasks;
using System.Linq;
using UnityEngine;

namespace BotBehavior
{
    public class NeedCollectEarned : BotCondition
    {
        public override TaskStatus OnUpdate()
        {
            Debug.Log("Check FullCollecters");
            return Bot.BehaviorTreeData.FullCollecters.Count() > 0 ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}


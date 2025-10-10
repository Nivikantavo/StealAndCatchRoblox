using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotBehavior
{
    public class CollectEarned : BotAction
    {
        private List<Transform> targets = new List<Transform>();

        public override void OnStart()
        {
            targets.AddRange(Bot.BehaviorTreeData.FullCollecters);
        }

        public override TaskStatus OnUpdate()
        {
            for (int i = 0;

            return targets.Count == 0 ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}


using BehaviorDesigner.Runtime.Tasks;
using System.Linq;
using UnityEngine;

namespace BotBehavior
{
    //TODO:
    //Реагировать при наличии 50% заполненных коллекторах от имеющихся мобов
    public class NeedCollectEarned : BotCondition
    {
        public override TaskStatus OnUpdate()
        {
            Debug.Log("Check FullCollecters");
            return Bot.BehaviorTreeData.FullCollecters.Count() > 0 ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}


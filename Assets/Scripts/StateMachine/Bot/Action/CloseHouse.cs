using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;

namespace BotBehavior
{
    public class CloseHouse : BotAction
    {
        private float _waitingTime = 1;
        private Coroutine _restartAwaiter;

        public override void OnStart()
        {
            _restartAwaiter = null;
            Bot.BehaviorTreeData.BotCharacterController.GoTo(Bot.BehaviorTreeData.LockHouseButton.position);
        }

        public override TaskStatus OnUpdate()
        {
            if(!Bot.BehaviorTreeData.IsHouseClosed && 
                Vector3.Distance(Bot.BehaviorTreeData.BotCharacterController.transform.position, Bot.BehaviorTreeData.LockHouseButton.position) < 0.1f &&
                _restartAwaiter == null)
            {
                _restartAwaiter = StartCoroutine(RestartColosening());
            }

            return Bot.BehaviorTreeData.IsHouseClosed ? TaskStatus.Success : TaskStatus.Running;
        }

        private IEnumerator RestartColosening()
        {
            yield return new WaitForSeconds(_waitingTime);
            Bot.BehaviorTreeData.BotCharacterController.GoTo(Bot.BehaviorTreeData.LockHouseButton.position + Vector3.left * 5);
            yield return new WaitForSeconds(_waitingTime);
            Bot.BehaviorTreeData.BotCharacterController.GoTo(Bot.BehaviorTreeData.LockHouseButton.position);
        }
    }
}

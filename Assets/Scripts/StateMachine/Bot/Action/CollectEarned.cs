using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotBehavior
{
    public class CollectEarned : BotAction
    {
        private List<Transform> _targets = new List<Transform>();
        private int _currentTargetIndex = 0;
        private int _currentOnWayIndex = -1;
        public override void OnStart()
        {
            _targets.AddRange(Bot.BehaviorTreeData.FullCollecters);
            _currentTargetIndex = 0;
            _currentOnWayIndex = _currentTargetIndex;
            Bot.BehaviorTreeData.BotCharacterController.GoTo(_targets[_currentTargetIndex].position);
        }

        public override TaskStatus OnUpdate()
        {
            Debug.Log(_currentTargetIndex);
            if (Vector3.Distance(Bot.transform.position, _targets[_currentOnWayIndex].position) < 1f)
            {
                _currentTargetIndex++;
            }

            if(_currentOnWayIndex != _currentTargetIndex)
            {
                Bot.BehaviorTreeData.BotCharacterController.GoTo(_targets[_currentTargetIndex].position);
                _currentOnWayIndex = _currentTargetIndex;
            }

            
            return _currentTargetIndex == _targets.Count - 1 ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}


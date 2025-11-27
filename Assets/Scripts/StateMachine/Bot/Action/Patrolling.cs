using BotBehavior;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Patrolling : BotAction
{
    private Vector3 _targetPoint;

    public override void OnStart()
    {
        _targetPoint = RandomNavmeshPointsCreator.GetRandomPoint(Bot.transform.position, 50, Bot.BehaviorTreeData.BotCharacterController.agent);
        Bot.BehaviorTreeData.BotCharacterController.GoTo(_targetPoint);
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(Bot.transform.position, _targetPoint) > Bot.BehaviorTreeData.CurrentRange)
        {
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}

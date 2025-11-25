using UnityEngine;
using UnityEngine.AI;

public static class RandomNavmeshPointsCreator
{
    public static Vector3 GetRandomPoint(Vector3 origin, float maxDistance, NavMeshAgent agent)
    {
        Vector3 randomPos = origin + Random.insideUnitSphere * maxDistance;

        NavMeshHit hit;

        bool foundPosition = NavMesh.SamplePosition(
            origin + Random.insideUnitSphere * maxDistance,
            out hit,
            maxDistance,
            NavMesh.AllAreas
        );

        if (!foundPosition)
            return Vector3.zero;

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(hit.position, path);
        bool canReachPoint = path.status == NavMeshPathStatus.PathComplete;
        return hit.position;
    }
}

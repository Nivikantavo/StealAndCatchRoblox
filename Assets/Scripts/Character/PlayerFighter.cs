using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : MonoBehaviour, IFighter
{
    [SerializeField] private float _maxDistance = 3f;
    [SerializeField] private float _maxAngle = 60f;
    [SerializeField] private LayerMask _targetLayer;

    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
    }

    public List<IFighter> Attack()
    {
        List<IFighter> targets = CheckAttackZone();
        foreach (IFighter target in targets)
        {
            target.TakeHit();
        }
        return targets;
    }

    public List<IFighter> CheckAttackZone()
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = transform.position + transform.forward * _maxDistance;
        float radius = Mathf.Tan(_maxAngle * Mathf.Deg2Rad) * _maxDistance;

        Collider[] allColliders = Physics.OverlapCapsule(startPoint, endPoint, radius, _targetLayer);

        List<IFighter> validTargets = new List<IFighter>();

        foreach (Collider col in allColliders)
        {
            if(col.TryGetComponent(out IFighter fighter))
            {
                if (fighter == this)
                    continue;

                Vector3 directionToTarget = (col.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, directionToTarget);

                if (angle <= _maxAngle)
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance <= _maxDistance)
                    {
                        validTargets.Add(fighter);
                    }
                }
            }
        }
        return validTargets;
    }

    public void TakeHit()
    {
        _player.TakeHit();
    }

    void OnDrawGizmosSelected()
    {
        // Визуализация зоны атаки
        Gizmos.color = Color.red;

        Vector3 forward = transform.forward;
        Vector3 left = Quaternion.Euler(0, -_maxAngle, 0) * forward;
        Vector3 right = Quaternion.Euler(0, _maxAngle, 0) * forward;

        // Линии границ конуса
        Gizmos.DrawRay(transform.position, left * _maxDistance);
        Gizmos.DrawRay(transform.position, right * _maxDistance);

        // Дуги для визуализации
        DrawWireArc(transform.position, transform.up, transform.forward, _maxAngle, _maxDistance);
    }

    void DrawWireArc(Vector3 position, Vector3 dir, Vector3 forward, float angle, float radius)
    {
        int segments = 20;
        float angleStep = angle * 2 / segments;

        Vector3 previousPoint = position + Quaternion.Euler(0, -angle, 0) * forward * radius;

        for (int i = 1; i <= segments; i++)
        {
            float currentAngle = -angle + angleStep * i;
            Vector3 currentPoint = position + Quaternion.Euler(0, currentAngle, 0) * forward * radius;
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
    }
}

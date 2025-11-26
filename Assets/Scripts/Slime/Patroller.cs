using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour, IMovementState
{
    [SerializeField] List<Transform> _points;
    [SerializeField] int _target;
    [SerializeField] int _distanceToChangeTarget;

    public Vector2 GetDirection()
    {
        Vector2 targetPosition = _points[_target].position;

        Vector2 direction = (Vector2)transform.position - targetPosition;
        
        if (direction.magnitude < _distanceToChangeTarget)
            _target = (_target + 1) % _points.Count;

        direction.Normalize();

        return direction;
    }
}

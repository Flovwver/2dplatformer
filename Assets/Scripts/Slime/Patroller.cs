using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour, IMovementState
{
    [SerializeField] List<Transform> _points;
    [SerializeField] int _target;
    [SerializeField] float _distanceToChangeTarget;

    public Vector2 GetDirection()
    {
        Vector2 targetPosition = _points[_target].position;

        Vector2 direction = targetPosition - (Vector2)transform.position;
        
        if (direction.magnitude < _distanceToChangeTarget)
            _target = (_target + 1) % _points.Count;

        direction.Normalize();

        return direction;
    }
}

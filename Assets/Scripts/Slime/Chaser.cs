using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Chaser : MonoBehaviour, IMovementState
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private float _chaseRange = 5f;

    public Vector2 GetDirection()
    {
        Vector2 direction = (Vector2)_playerPosition.position - (Vector2)transform.position;

        direction.Normalize();

        return direction;
    }

    public bool IsPlayerInChaseRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _playerPosition.position);
        return distanceToPlayer < _chaseRange;
    }
}

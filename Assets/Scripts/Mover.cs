using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _smooth = 20f;

    private float _moveHorizontalDirection = 1f;

    private Rigidbody2D _rigidbody;

    public float LinearVelocityX => _rigidbody.linearVelocityX;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        float targetVelocityX = _moveHorizontalDirection * _speed;

        _rigidbody.linearVelocityX = Mathf.Lerp(_rigidbody.linearVelocityX, targetVelocityX, _smooth * Time.fixedDeltaTime);

        _moveHorizontalDirection = 0f;
    }

    public void StartMove(float moveDirection)
    {
        _moveHorizontalDirection = Mathf.Sign(moveDirection);
    }

    public void Push(Vector2 source, float forceMagnitude)
    {
        Vector3 recoilAngel = new(0f, 0f, 45f);
        Vector2 rawDirection = (Vector2)transform.position - source;
        Vector2 horizontalDirection = new(Mathf.Sign(rawDirection.x), 0f);

        Vector2 recoilDirection = Quaternion.Euler(Mathf.Sign(rawDirection.x) * recoilAngel) * horizontalDirection;

        _rigidbody.linearVelocity += recoilDirection * forceMagnitude;
    }
}
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

    private void FixedUpdate()
    {
        float targetVelocityX = _moveHorizontalDirection * _speed;

        _rigidbody.linearVelocityX = Mathf.Lerp(_rigidbody.linearVelocityX, targetVelocityX, _smooth * Time.fixedDeltaTime);

        _moveHorizontalDirection = 0f;
    }

    public void Move(float moveDirection)
    {
        _moveHorizontalDirection = Mathf.Sign(moveDirection);
    }
}
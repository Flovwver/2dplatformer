using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _moveHorizontalDirection = 0f;
    [SerializeField] private float _smooth = 20f;

    private Rigidbody2D _rigidbody;

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

    public void Move(MoveDirection moveDirection)
    {
        if (moveDirection == MoveDirection.Left)
        {
            _moveHorizontalDirection = -1f;
        }
        else if (moveDirection == MoveDirection.Right)
        {
            _moveHorizontalDirection = 1f;
        }
    }
}
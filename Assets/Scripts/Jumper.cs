using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] CollisionChecker _collisionChecker;
    [SerializeField] bool _isJumpStarted;
    [SerializeField] int _jumpCount = 0;
    [SerializeField] int _maxJumpCount = 1;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_collisionChecker.IsGrounded)
            _jumpCount = 0;

        if (_isJumpStarted)
        {
            Jump();
            _isJumpStarted = false;
        }
    }

    public void StartJump()
    {
        _isJumpStarted = true;
    }

    private void Jump()
    {
        if (_collisionChecker.IsGrounded || _jumpCount < _maxJumpCount)
        {
            _rigidbody.linearVelocityY = _jumpForce;
            _jumpCount++;
        }
    }
}

using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] CollisionChecker _collisionChecker;
    [SerializeField] bool _isJumpStarted;
    [SerializeField] int _jumpCount = 0;
    [SerializeField] int _maxJumpCount = 1;
    [SerializeField] private float _groundResetDelay = 0.1f;

    [SerializeField] private float _groundResetTimer = 0f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_groundResetTimer > 0)
            _groundResetTimer -= Time.fixedDeltaTime;

        if (_groundResetTimer <= 0 && _collisionChecker.IsGrounded)
            _jumpCount = 0;

        if (_isJumpStarted)
        {
            _groundResetTimer = _groundResetDelay;
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

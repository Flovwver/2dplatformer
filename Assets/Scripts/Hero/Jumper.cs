using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] bool _isJumpStarted;
    [SerializeField] int _jumpCount = 0;
    [SerializeField] int _maxJumpCount = 1;
    [SerializeField] private float _groundResetDelay = 0.3f;

    [SerializeField] private float _groundResetTimer = 0f;

    private Rigidbody2D _rigidbody;
    private CollisionChecker _collisionChecker;
    private JumpAnimator _jumpAnimator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionChecker = GetComponent<CollisionChecker>();
        _jumpAnimator = GetComponent<JumpAnimator>();
    }

    private void FixedUpdate()
    {
        bool isGrounded = _collisionChecker.IsGrounded;

        if (_groundResetTimer > 0)
            _groundResetTimer -= Time.fixedDeltaTime;

        if (_groundResetTimer <= 0 && isGrounded)
            _jumpCount = 0;

        if (_isJumpStarted)
        {
            _groundResetTimer = _groundResetDelay;
            Jump();
            _isJumpStarted = false;
        }

        _jumpAnimator.Animate(_rigidbody.linearVelocityY, isGrounded);
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

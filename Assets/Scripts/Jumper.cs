using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundDetector))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private bool _isJumpStarted;
    [SerializeField] private int _jumpCount = 0;
    [SerializeField] private int _maxJumpCount = 1;
    [SerializeField] private float _groundResetDelay = 0.3f;
    [SerializeField] private float _groundResetTimer = 0f;

    private Rigidbody2D _rigidbody;
    private GroundDetector _collisionChecker;

    public float LinearVelocityY => _rigidbody.linearVelocityY;

    public bool IsGrounded => _collisionChecker.IsGrounded();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionChecker = GetComponent<GroundDetector>();
    }

    public void StartJump()
    {
        _isJumpStarted = true;
    }

    public void Jump()
    {
        if (_isJumpStarted)
        {
            _groundResetTimer = _groundResetDelay;

            if (_collisionChecker.IsGrounded() || _jumpCount < _maxJumpCount)
            {
                _rigidbody.linearVelocityY = _jumpForce;
                _jumpCount++;
            }

            _isJumpStarted = false;
        }
    }

    public void UpdateFields()
    {
        if (_groundResetTimer > 0)
            _groundResetTimer -= Time.fixedDeltaTime;

        if (_groundResetTimer <= 0 && _collisionChecker.IsGrounded())
            _jumpCount = 0;
    }
}

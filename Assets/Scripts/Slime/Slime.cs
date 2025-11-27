using UnityEngine;

[RequireComponent(typeof(SlimeAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(GroundDetector))]
public class Slime : MonoBehaviour
{
    [SerializeField] private float _lastDirection = -1f;
    [SerializeField] private Patroller _patroller;
    [SerializeField] private Chaser _chaser;

    [SerializeField] private float _jumpCooldown = 0.3f;
    [SerializeField] private float _jumpCooldownTimer = 0f;

    private SlimeAnimator _slimeAnimator;
    private Health _health;
    private Mover _mover;
    private GroundDetector _groundDetector;
    private IMovementState _movementState;

    private void Awake()
    {
        _slimeAnimator = GetComponent<SlimeAnimator>();
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _groundDetector = GetComponentInChildren<GroundDetector>();

        _movementState = _patroller;
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void FixedUpdate()
    {
        AnimateMove();
        ChoseMovementState();
        MoveToTarget();
    }

    private void ChoseMovementState()
    {
        if (_chaser.IsPlayerInChaseRange())
            _movementState = _chaser;
        else
            _movementState = _patroller;
    }

    private void MoveToTarget()
    {
        Vector2 direction = _movementState.GetDirection();

        if (_jumpCooldownTimer - Time.fixedDeltaTime > 0)
            _jumpCooldownTimer -= Time.fixedDeltaTime;
        else
            _jumpCooldownTimer = 0;

        _mover.Move(Mathf.Sign(direction.x));

        if(direction.y > 0 && _groundDetector.IsGrounded() && _jumpCooldownTimer <= 0)
        {
            _jumpCooldownTimer = _jumpCooldown;

            _mover.Jump();
            Debug.Log("jump");
        }
    }

    private void OnDied()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private void AnimateMove()
    {
        float direction = Mathf.Sign(_mover.LinearVelocityX);

        if (Mathf.Abs(_mover.LinearVelocityX) > 1f)
        {
            if (_lastDirection != direction)
                _slimeAnimator.AnimateMove(direction);

            _lastDirection = direction;
        }
    }
}

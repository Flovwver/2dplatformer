using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Mover _mover;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CollisionChecker _collisionChecker;

    private Rigidbody2D _rigidbody;

    private readonly string _speed = "Speed";
    private readonly string _verticalVelocity = "VerticalVelocity";
    private readonly string _IsGrounded = "IsGrounded";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _mover.Moved += OnMoved;
    }

    private void OnDisable()
    {
        _mover.Moved -= OnMoved;
    }

    private void Update()
    {
        float speed = Mathf.Abs(_rigidbody.linearVelocityX);
        float verticalVelocity = _rigidbody.linearVelocityY;

        _animator.SetFloat(_speed, speed);
        _animator.SetFloat(_verticalVelocity, verticalVelocity);
        _animator.SetBool(_IsGrounded, _collisionChecker.IsGrounded);
    }

    private void OnMoved(MoveDirection moveDirection)
    {
        if (moveDirection == MoveDirection.Left)
        {
            _spriteRenderer.flipX = true;
        }
        else if (moveDirection == MoveDirection.Right)
        {
            _spriteRenderer.flipX = false;
        }
    }
}

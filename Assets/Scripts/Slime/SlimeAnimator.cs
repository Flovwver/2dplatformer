using UnityEngine;

public class SlimeAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Mover _mover;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody;

    private readonly string _speed = "Speed";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float speed = Mathf.Abs(_rigidbody.linearVelocityX);

        _animator.SetFloat(_speed, speed);
    }

    private void OnEnable()
    {
        _mover.Moved += OnMoved;
    }

    private void OnDisable()
    {
        _mover.Moved -= OnMoved;
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

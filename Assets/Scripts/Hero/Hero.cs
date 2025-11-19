using UnityEngine;

[RequireComponent(typeof(Inputer))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(HeroAnimator))]
[RequireComponent(typeof(Health))]
public class Hero : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;

    private Inputer _inputer;
    private Mover _mover;
    private Jumper _jumper;
    private HeroAnimator _heroAnimator;
    private Health _health;

    private void Awake()
    {
        _inputer = GetComponent<Inputer>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _heroAnimator = GetComponent<HeroAnimator>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        _heroAnimator.AnimateMove(_mover.LinearVelocityX);
        _heroAnimator.AnimateJump(_jumper.LinearVelocityY, _jumper.IsGrounded);
    }

    private void OnEnable()
    {
        _inputer.MoveLeftPressed += OnMoveLeftPressed;
        _inputer.MoveRightPressed += OnMoveRightPressed;
        _inputer.JumpPressed += OnJumpPressed;
        _inputer.AttackPressed += OnAttackPressed;
        _health.Died += OnDied;
        _health.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _inputer.MoveLeftPressed -= OnMoveLeftPressed;
        _inputer.MoveRightPressed -= OnMoveRightPressed;
        _inputer.JumpPressed -= OnJumpPressed;
        _inputer.AttackPressed -= OnAttackPressed;
        _health.Died -= OnDied;
        _health.Damaged -= OnDamaged;
    }

    private void FixedUpdate()
    {
        _mover.Move();
        _jumper.Jump();
        _jumper.UpdateFields();
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    private void OnMoveLeftPressed()
    {
        float leftDirection = -1f;

        _mover.StartMove(leftDirection);
    }

    private void OnMoveRightPressed()
    {
        float rightDirection = 1f;

        _mover.StartMove(rightDirection);
    }

    private void OnJumpPressed()
    {
        _jumper.StartJump();
    }

    private void OnAttackPressed()
    {
        _attacker.StartAttack();
    }

    private void OnDamaged(Vector2 source)
    {
        float damageForce = 5f;

        _mover.Push(source, damageForce);
    }
}

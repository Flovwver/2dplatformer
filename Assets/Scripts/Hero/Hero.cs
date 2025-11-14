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
    }

    private void OnDisable()
    {
        _inputer.MoveLeftPressed -= OnMoveLeftPressed;
        _inputer.MoveRightPressed -= OnMoveRightPressed;
        _inputer.JumpPressed -= OnJumpPressed;
        _inputer.AttackPressed -= OnAttackPressed;
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    private void OnMoveLeftPressed()
    {
        float leftDirection = -1f;

        _mover.Move(leftDirection);
    }

    private void OnMoveRightPressed()
    {
        float rightDirection = 1f;

        _mover.Move(rightDirection);
    }

    private void OnJumpPressed()
    {
        _jumper.StartJump();
    }

    private void OnAttackPressed()
    {
        _attacker.StartAttack();
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Inputer))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(HeroAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(Attacker))]
public class Hero : MonoBehaviour
{
    [SerializeField] private float _groundedWaitTime = 0.3f;

    private Inputer _inputer;
    private Mover _mover;
    private HeroAnimator _heroAnimator;
    private Health _health;
    private GroundDetector _groundDetector;
    private Attacker _attacker;

    private void Awake()
    {
        _inputer = GetComponent<Inputer>();
        _mover = GetComponent<Mover>();
        _heroAnimator = GetComponent<HeroAnimator>();
        _health = GetComponent<Health>();
        _groundDetector = GetComponent<GroundDetector>();
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
        _health.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
        _health.Damaged -= OnDamaged;
    }

    private void FixedUpdate()
    {
        if (_inputer.GetIsJump() && _groundDetector.IsGrounded())
        {
            _mover.Jump();
            StartCoroutine(AnimateFall());
        }

        if (_inputer.GetIsAttack())
        {
            _attacker.Attack();
            _heroAnimator.AnimateAtack();
        }

        _heroAnimator.AnimateMove(_inputer.Direction, _inputer.Direction != 0);
        _mover.Move(_inputer.Direction);
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    private void OnDamaged(Vector2 source)
    {
        _mover.Knockback(source);
    }

    private IEnumerator AnimateFall()
    {
        bool isGrounded = _groundDetector.IsGrounded();

        while (isGrounded == false)
        {
            _heroAnimator.AnimateJump(_mover.LinearVelocityY, isGrounded);
            yield return null;
         
            isGrounded = _groundDetector.IsGrounded();
        }

        _heroAnimator.AnimateJump(_mover.LinearVelocityY, isGrounded);
    }
}

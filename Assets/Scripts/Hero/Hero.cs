using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Inputer))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(HeroAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(GroundDetector))]
public class Hero : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private bool _lastIsGrounded;
    [SerializeField] private float _groundedWaitTime = 0.3f;

    private Inputer _inputer;
    private Mover _mover;
    private HeroAnimator _heroAnimator;
    private Health _health;
    private GroundDetector _groundDetector;

    private void Awake()
    {
        _inputer = GetComponent<Inputer>();
        _mover = GetComponent<Mover>();
        _heroAnimator = GetComponent<HeroAnimator>();
        _health = GetComponent<Health>();
        _groundDetector = GetComponent<GroundDetector>();
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

    private void Update()
    {
        StartAnimateFall();
    }

    private void FixedUpdate()
    {
        if (_inputer.GetIsJump() && _groundDetector.IsGrounded())
            _mover.Jump();

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

    private void StartAnimateFall()
    {
        bool isGrounded = _groundDetector.IsGrounded();

        if (_lastIsGrounded != isGrounded && isGrounded == false)
        {
            _lastIsGrounded = _groundDetector.IsGrounded();
            StartCoroutine(AnimateFall());
        }

        _lastIsGrounded = isGrounded;
    }

    private IEnumerator AnimateFall()
    {
        while(_groundDetector.IsGrounded() == false)
        {
            _heroAnimator.AnimateJump(_mover.LinearVelocityY, _groundDetector.IsGrounded());
            yield return null;
        }

        _heroAnimator.AnimateJump(_mover.LinearVelocityY, true);
        _lastIsGrounded = true;
    }
}

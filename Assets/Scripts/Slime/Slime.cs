using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(SlimeAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Patroller))]
public class Slime : MonoBehaviour
{
    [SerializeField] private float _lastDirection = -1f;

    private Mover _mover;
    private SlimeAnimator _slimeAnimator;
    private Health _health;
    private Jumper _jumper;
    private Patroller _patroller;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _slimeAnimator = GetComponent<SlimeAnimator>();
        _health = GetComponent<Health>();
        _jumper = GetComponent<Jumper>();
        _patroller = GetComponent<Patroller>();
    }

    private void Update()
    {
        AnimateMove();
        _patroller.SelectMovingMode();
    }

    private void FixedUpdate()
    {
        _mover.Move();
        _jumper.Jump();
        _jumper.UpdateFields();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
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

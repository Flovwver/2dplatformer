using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(SlimeAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Patroller))]
public class Slime : MonoBehaviour
{
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
        _slimeAnimator.AnimateMove(_mover.LinearVelocityX);
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
}

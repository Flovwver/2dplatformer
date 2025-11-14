using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(SlimeAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Jumper))]
public class Slime : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private bool _isMove;
    [SerializeField] private float _moveTimeToSide = 5f;

    [Header("Detection")]
    [SerializeField] private float _viewDistance = 10f;
    [SerializeField] private float _viewAngle = 30f;
    [SerializeField] private LayerMask _heroLayer;
    [SerializeField] private Transform _hero;
    [SerializeField] private bool _isChasing;
    [SerializeField] private float _minJumpDistance;

    private Mover _mover;
    private SlimeAnimator _slimeAnimator;
    private Health _health;
    private Jumper _jumper;

    private Coroutine _patrolRoutine;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _slimeAnimator = GetComponent<SlimeAnimator>();
        _health = GetComponent<Health>();
        _jumper = GetComponent<Jumper>();
    }

    private void Start()
    {
        _patrolRoutine = StartCoroutine(Patrol());
    }

    private void Update()
    {
        _slimeAnimator.AnimateMove(_mover.LinearVelocityX);

        if (_hero == null)
            return;

        if (CanSeeHero())
        {
            if (!_isChasing)
            {
                _isChasing = true;
                if (_patrolRoutine != null)
                {
                    StopCoroutine(_patrolRoutine);
                    _patrolRoutine = null;
                }
            }

            MoveTowardHero();
        }
        else
        {
            if (_isChasing)
            {
                _isChasing = false;
                _patrolRoutine = StartCoroutine(Patrol());
            }
        }
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private IEnumerator Patrol()
    {
        float currentMoveDirection = -1f;

        while (_isMove)
        {
            if (currentMoveDirection == -1f)
                currentMoveDirection = 1f;
            else
                currentMoveDirection = -1f;

            for (float moveTimer = 0; moveTimer < _moveTimeToSide; moveTimer += Time.deltaTime)
            {
                if (_isChasing)
                    yield break;

                _mover.Move(currentMoveDirection);
                yield return null;
            }
        }
    }

    private bool CanSeeHero()
    {
        Vector2 directionToHero = _hero.position - transform.position;
        float distance = directionToHero.magnitude;

        if (distance > _viewDistance)
            return false;

        directionToHero.Normalize();

        Vector2 facingDir = transform.right * Mathf.Sign(_mover.transform.localScale.x);

        float angle = Vector2.Angle(facingDir, directionToHero);

        return angle <= _viewAngle;
    }

    private void MoveTowardHero()
    {
        float direction = Mathf.Sign(_hero.position.x - transform.position.x);
        bool isHeroUpper = _hero.position.y > transform.position.y;
        bool isHeroCloser = Vector3.Distance(_hero.position, transform.position) < _minJumpDistance;

        if (isHeroUpper && isHeroCloser == false)
        {
            _jumper.StartJump();
        }

        _mover.Move(direction);
    }

    private void OnDied()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}

using System.Collections;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private bool _isMove;
    [SerializeField] private float _viewDistance = 10f;
    [SerializeField] private float _viewAngle = 30f;
    [SerializeField] private LayerMask _heroLayer;
    [SerializeField] private Transform _hero;
    [SerializeField] private bool _isChasing;
    [SerializeField] private float _minJumpDistance;
    [SerializeField] private float _moveTimeToSide = 5f;
    [SerializeField] private float _currentMoveDirection = -1f;

    private Coroutine _patrolRoutine;

    private void Start()
    {
        _patrolRoutine = StartCoroutine(Patrol());
    }

    private void SelectMovingMode()
    {
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

    private IEnumerator Patrol()
    {

        while (_isMove)
        {
            for (float moveTimer = 0; moveTimer < _moveTimeToSide; moveTimer += Time.deltaTime)
            {
                if (_isChasing)
                    yield break;

                _mover.StartMove(_currentMoveDirection);
                yield return null;
            }

            if (_currentMoveDirection == -1f)
                _currentMoveDirection = 1f;
            else
                _currentMoveDirection = -1f;
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

        _mover.StartMove(direction);
    }
}

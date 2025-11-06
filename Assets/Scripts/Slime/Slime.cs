using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(SlimeAnimator))]
public class Slime : MonoBehaviour
{
    [SerializeField] private bool _isMove;
    [SerializeField] private float _moveTimeToSide = 5f;

    private Mover _mover;
    private SlimeAnimator _slimeAnimator;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _slimeAnimator = GetComponent<SlimeAnimator>();
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void Update()
    {
        _slimeAnimator.AnimateMove(_mover.LinearVelocityX);
    }

    private IEnumerator Move()
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
                _mover.Move(currentMoveDirection);
                yield return null;
            }
        }
    }
}

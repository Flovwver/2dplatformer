using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class SlimeController : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private bool _isMove;
    [SerializeField] private float _moveTimeToSide = 5f;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        MoveDirection currentMoveDirection = MoveDirection.Right;

        while (_isMove)
        {
            if (currentMoveDirection == MoveDirection.Left)
                currentMoveDirection = MoveDirection.Right;
            else
                currentMoveDirection = MoveDirection.Left;

            for (float moveTimer = 0; moveTimer < _moveTimeToSide; moveTimer += Time.deltaTime)
            {
                _mover.Move(currentMoveDirection);
                yield return null;
            }
        }
    }
}

using UnityEngine;

public static class MoveAnimatorData
{
    public static class Parameters
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    }
}

[RequireComponent(typeof(Animator))]
public class MoveAnimator : MonoBehaviour
{
    [SerializeField] private float _speedThreshold = 10f;
    [SerializeField] private float _lastDirection = 1f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Animate(float linearVelocityX)
    {
        float speed = Mathf.Abs(linearVelocityX);
        float direction = _lastDirection;

        if (speed > 1f)
        {
            direction = Mathf.Sign(linearVelocityX);
            _lastDirection = direction;
        }

        SetSpeed(speed);
        Rotate(direction);
    }

    private void Rotate(float moveDirection)
    {
        Quaternion leftDirtection = Quaternion.Euler(0f, 180f, 0f);
        Quaternion rightDirtection = Quaternion.Euler(0f, 0f, 0f);

        if (moveDirection < 0f)
        {
            transform.rotation = leftDirtection;
        }
        else if (moveDirection > 0f)
        {
            transform.rotation = rightDirtection;
        }
    }

    private void SetSpeed(float speed)
    {
        if (speed < 0)
            speed = 0;
        else if (speed > _speedThreshold)
            speed = _speedThreshold;

        _animator.SetFloat(MoveAnimatorData.Parameters.Speed, speed);
    }
}

using UnityEngine;

public static class HeroAnimatorData
{
    public static class Parameters
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int VerticalVelocity = Animator.StringToHash(nameof(VerticalVelocity));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    }
}

[RequireComponent(typeof(Animator))]
public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private float _minVerticalVelocity = -5f;
    [SerializeField] private float _maxVerticalVelocity = 5f;
    [SerializeField] private float _speedThreshold = 10f;
    [SerializeField] private float _lastDirection = 1f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void AnimateJump(float verticalVelocityX, bool isGrounded)
    {
        SetVerticalVelocity(verticalVelocityX);
        SetIsGrounded(isGrounded);
    }

    public void AnimateMove(float linearVelocityX)
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

    private void SetVerticalVelocity(float verticalVelocity)
    {
        float clampedVelocity = Mathf.Clamp(verticalVelocity, _minVerticalVelocity, _maxVerticalVelocity);

        _animator.SetFloat(HeroAnimatorData.Parameters.VerticalVelocity, clampedVelocity);
    }

    private void SetIsGrounded(bool isGrounded)
    {
        _animator.SetBool(HeroAnimatorData.Parameters.IsGrounded, isGrounded);
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

        _animator.SetFloat(HeroAnimatorData.Parameters.Speed, speed);
    }
}
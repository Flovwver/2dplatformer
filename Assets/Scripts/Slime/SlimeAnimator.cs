using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Flipper))]
public class SlimeAnimator : MonoBehaviour
{
    [SerializeField] private float _speedThreshold = 10f;
    [SerializeField] private float _lastDirection = 1f;

    private Animator _animator;
    private Flipper _flipper;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _flipper = GetComponent<Flipper>();
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
        _flipper.Rotate(direction);
    }

    private void SetSpeed(float speed)
    {
        if (speed < 0)
            speed = 0;
        else if (speed > _speedThreshold)
            speed = _speedThreshold;

        _animator.SetFloat(SlimeAnimatorData.Parameters.Speed, speed);
    }
}

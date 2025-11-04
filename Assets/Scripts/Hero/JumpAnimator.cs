using UnityEngine;

public static class JumpAnimatorData
{
    public static class Parameters
    {
        public static readonly int VerticalVelocity = Animator.StringToHash(nameof(VerticalVelocity));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    }
}

[RequireComponent(typeof(Animator))]
public class JumpAnimator : MonoBehaviour
{
    [SerializeField] private float _minVerticalVelocity = -5f;
    [SerializeField] private float _maxVerticalVelocity = 5f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Animate(float verticalVelocityX, bool isGrounded)
    {
        SetVerticalVelocity(verticalVelocityX);
        SetIsGrounded(isGrounded);
    }

    private void SetVerticalVelocity(float verticalVelocity)
    {
        float clampedVelocity = Mathf.Clamp(verticalVelocity, _minVerticalVelocity, _maxVerticalVelocity);

        _animator.SetFloat(JumpAnimatorData.Parameters.VerticalVelocity, clampedVelocity);
    }

    private void SetIsGrounded(bool isGrounded)
    {
        _animator.SetBool(JumpAnimatorData.Parameters.IsGrounded, isGrounded);
    }
}

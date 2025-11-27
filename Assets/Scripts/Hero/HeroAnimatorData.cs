using UnityEngine;

public static class HeroAnimatorData
{
    public static class Parameters
    {
        public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
        public static readonly int VerticalVelocity = Animator.StringToHash(nameof(VerticalVelocity));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}

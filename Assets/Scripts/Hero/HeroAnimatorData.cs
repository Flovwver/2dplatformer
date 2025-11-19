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

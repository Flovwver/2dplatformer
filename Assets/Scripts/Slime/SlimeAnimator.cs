using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Flipper))]
public class SlimeAnimator : MonoBehaviour
{
    [SerializeField] private float _speedThreshold = 10f;

    private Animator _animator;
    private Flipper _flipper;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _flipper = GetComponent<Flipper>();
    }

    public void AnimateMove(float direction)
    {
        _flipper.Rotate(direction);
    }
}

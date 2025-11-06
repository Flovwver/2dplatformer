using UnityEngine;

[RequireComponent(typeof(Inputer))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(HeroAnimator))]
public class Hero : MonoBehaviour
{
    private Inputer _inputer;
    private Mover _mover;
    private Jumper _jumper;
    private HeroAnimator _heroAnimator;

    private void Awake()
    {
        _inputer = GetComponent<Inputer>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _heroAnimator = GetComponent<HeroAnimator>();
    }

    private void Update()
    {
        _heroAnimator.AnimateMove(_mover.LinearVelocityX);
        _heroAnimator.AnimateJump(_jumper.LinearVelocityY, _jumper.IsGrounded);
    }

    private void OnEnable()
    {
        _inputer.MoveLeftPressed += OnMoveLeftPressed;
        _inputer.MoveRightPressed += OnMoveRightPressed;
        _inputer.JumpPressed += OnJumpPressed;
    }

    private void OnDisable()
    {
        _inputer.MoveLeftPressed -= OnMoveLeftPressed;
        _inputer.MoveRightPressed -= OnMoveRightPressed;
        _inputer.JumpPressed -= OnJumpPressed;
    }

    private void OnMoveLeftPressed()
    {
        float leftDirection = -1f;

        _mover.Move(leftDirection);
    }

    private void OnMoveRightPressed()
    {
        float rightDirection = 1f;

        _mover.Move(rightDirection);
    }

    private void OnJumpPressed()
    {
        _jumper.StartJump();
    }
}

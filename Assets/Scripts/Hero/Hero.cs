using UnityEngine;

public class Hero : MonoBehaviour
{
    private Inputer _inputer;
    private Mover _mover;
    private Jumper _jumper;

    private void Awake()
    {
        _inputer = GetComponent<Inputer>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
    }

    private void OnEnable()
    {
        _inputer.MovePressed += OnMovePressed;
        _inputer.JumpPressed += OnJumpPressed;
    }

    private void OnDisable()
    {
        _inputer.MovePressed -= OnMovePressed;
        _inputer.JumpPressed -= OnJumpPressed;
    }

    private void OnMovePressed(MoveDirection moveDirection)
    {
        _mover.Move(moveDirection);
    }

    private void OnJumpPressed()
    {
        _jumper.StartJump();
    }
}

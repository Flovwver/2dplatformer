using UnityEngine;
using UnityEngine.InputSystem;

public class Inputer : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;

    private void Update()
    {
        if (Keyboard.current.leftArrowKey.isPressed)
            _mover.Move(MoveDirection.Left);

        if (Keyboard.current.rightArrowKey.isPressed)
            _mover.Move(MoveDirection.Right);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            _jumper.StartJump();
    }
}

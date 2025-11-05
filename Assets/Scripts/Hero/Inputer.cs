using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputer : MonoBehaviour
{
    public event Action<MoveDirection> MovePressed;
    public event Action JumpPressed;

    private void Update()
    {
        if (Keyboard.current.leftArrowKey.isPressed)
            MovePressed?.Invoke(MoveDirection.Left);

        if (Keyboard.current.rightArrowKey.isPressed)
            MovePressed?.Invoke(MoveDirection.Right);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            JumpPressed?.Invoke();
    }
}

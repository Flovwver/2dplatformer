using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputer : MonoBehaviour
{
    public event Action MoveLeftPressed;
    public event Action MoveRightPressed;
    public event Action JumpPressed;

    private void Update()
    {
        if (Keyboard.current.leftArrowKey.isPressed)
            MoveLeftPressed?.Invoke();

        if (Keyboard.current.rightArrowKey.isPressed)
            MoveRightPressed?.Invoke();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            JumpPressed?.Invoke();
    }
}

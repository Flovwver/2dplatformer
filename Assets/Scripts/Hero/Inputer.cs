using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputer : MonoBehaviour
{
    public event Action MoveLeftPressed;
    public event Action MoveRightPressed;
    public event Action MoveLeftReleased;
    public event Action MoveRightReleased;
    public event Action JumpPressed;
    public event Action AttackPressed;

    private void Update()
    {
        if (Keyboard.current.leftArrowKey.isPressed)
            MoveLeftPressed?.Invoke();

        if (Keyboard.current.rightArrowKey.isPressed)
            MoveRightPressed?.Invoke();

        if (Keyboard.current.leftArrowKey.wasReleasedThisFrame)
            MoveLeftReleased?.Invoke();

        if (Keyboard.current.rightArrowKey.wasReleasedThisFrame)
            MoveRightReleased?.Invoke();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            JumpPressed?.Invoke();

        if (Keyboard.current.ctrlKey.wasPressedThisFrame)
            AttackPressed?.Invoke();
    }
}

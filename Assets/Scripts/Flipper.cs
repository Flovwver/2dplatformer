using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private RotatableComponents _rotatableComponents;

    public void Rotate(float moveDirection)
    {
        if (_rotatableComponents == null)
        {
            Debug.LogWarning("RotatableComponents reference is not set.");
            return;
        }

        Quaternion leftDirtection = Quaternion.Euler(0f, 180f, 0f);
        Quaternion rightDirtection = Quaternion.Euler(0f, 0f, 0f);

        if (moveDirection < 0f)
        {
            _rotatableComponents.transform.rotation = leftDirtection;
        }
        else if (moveDirection > 0f)
        {
            _rotatableComponents.transform.rotation = rightDirtection;
        }
    }
}

using UnityEngine;

public class Flipper : MonoBehaviour
{
    public void Rotate(float moveDirection)
    {
        Quaternion leftDirtection = Quaternion.Euler(0f, 180f, 0f);
        Quaternion rightDirtection = Quaternion.Euler(0f, 0f, 0f);

        if (moveDirection < 0f)
        {
            transform.rotation = leftDirtection;
        }
        else if (moveDirection > 0f)
        {
            transform.rotation = rightDirtection;
        }
    }
}

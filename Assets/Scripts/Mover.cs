using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speedHorizontal = 5f;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _knockbackForce;

    private Rigidbody2D _rigidbody;

    public float LinearVelocityX => _rigidbody.linearVelocityX;
    public float LinearVelocityY => _rigidbody.linearVelocityY;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.linearVelocityY += _jumpForce;
    }

    public void Move(float direction)
    {
        if (direction == 0f)
            return;
        else
            direction = Mathf.Sign(direction);

        _rigidbody.linearVelocityX = _speedHorizontal * direction * Time.fixedDeltaTime;
    }

    public void Knockback(Vector2 source)
    {
        Vector3 angel = new(0f, 0f, 45f);
        Vector2 directionToSource = (Vector2)transform.position - source;
        Vector2 horizontalDirection = new(Mathf.Sign(directionToSource.x), 0f);
        
        Vector2 direction = Quaternion.Euler(Mathf.Sign(directionToSource.x) * angel) * horizontalDirection;

        _rigidbody.linearVelocity += direction * _knockbackForce;
    }
}
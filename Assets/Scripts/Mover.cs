using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 0.05f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private float _groundDistance = 0.5f;
    [SerializeField] private LayerMask _groundMask;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Keyboard.current.rightArrowKey.isPressed)
            Move(Vector2.right);
        else if (Keyboard.current.leftArrowKey.isPressed)
            Move(Vector2.left);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Jump();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundDistance, _groundMask);

        _isGrounded = hit.collider != null;

        Debug.DrawRay(transform.position, Vector2.down * _groundDistance,
            _isGrounded ? Color.green : Color.red);
    }


    private void Move(Vector2 direction)
    {
        Vector3 velocity = direction * _speed;
        _rigidbody.MovePosition(transform.position + velocity);
    }

    private void Jump()
    {
        if (_isGrounded)
            _rigidbody.AddForce(Vector3.up * _jumpForce);
    }
}

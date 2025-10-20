using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 0.05f;
    [SerializeField] private float _jumpHeight = 5f;
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private float _jumpTime = 0.5f;
    [SerializeField] private float _groundDistance = 0.5f;
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _coyoteTimeCounter;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private bool _jumpPressed;

    private Rigidbody2D _rigidbody;
    private Coroutine _jumpRoutine;

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

        if (_jumpPressed)
            _jumpRoutine = StartCoroutine(Jump());
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(transform.position, _groundDistance, _groundMask);

        if (_isGrounded)
            _coyoteTimeCounter = _coyoteTime;
        else
            _coyoteTimeCounter -= Time.deltaTime;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _jumpPressed = true;
        }
    }


    private void Move(Vector2 direction)
    {
        Vector3 velocity = direction * _speed;
        _rigidbody.MovePosition(transform.position + velocity);
    }

    private IEnumerator Jump()
    {
        _jumpPressed = false;
        
        if (_isGrounded || _coyoteTimeCounter > 0)
        {
            _isGrounded = false;

            for (float currentJumpTime = 0; currentJumpTime < _jumpTime; currentJumpTime += Time.fixedDeltaTime)
            {
                float normalizedJumpTime = Mathf.Clamp01(currentJumpTime / _jumpTime);
                float jumpAmount = _jumpCurve.Evaluate(normalizedJumpTime) * _jumpHeight * Time.fixedDeltaTime;

                Vector3 velocity = Vector2.up * jumpAmount;
                _rigidbody.MovePosition(transform.position + velocity);
                yield return null;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _groundDistance);
    }
}

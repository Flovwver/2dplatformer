using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 0.05f;
    [SerializeField] private float _jumpHeight = 5f;
    [SerializeField] private float _jumpTime = 0.5f;
    [SerializeField] private float _groundDistance = 0.5f;
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _coyoteTimeCounter;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private bool _isJump = false;

    [SerializeField] private bool _jumpPressed;

    private Coroutine _jumpRoutine;

    private bool IsGrounded => Physics2D.OverlapCircle(transform.position, _groundDistance, _groundMask);

    private void FixedUpdate()
    {
        if (Keyboard.current.rightArrowKey.isPressed)
            Move(Vector2.right);
        else if (Keyboard.current.leftArrowKey.isPressed)
            Move(Vector2.left);

        if (_jumpPressed && _jumpRoutine == null && _isJump == false)
        {
            _isJump = true;
            _jumpRoutine = StartCoroutine(Jump());
        }
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _jumpPressed = true;
        }

        if (IsGrounded)
            _coyoteTimeCounter = _coyoteTime;
        else
            _coyoteTimeCounter -= Time.deltaTime;

    }

    private void Move(Vector2 direction)
    {
        Vector3 velocity = direction * _speed;
        transform.position += velocity;
    }

    private IEnumerator Jump()
    {
        _jumpPressed = false;

        if (IsGrounded || _coyoteTimeCounter > 0)
        {
            float heightOnStartJump = transform.position.y;

            _coyoteTimeCounter = 0;

            for (float currentJumpTime = 0; currentJumpTime < _jumpTime; currentJumpTime += Time.fixedDeltaTime)
            {
                if (currentJumpTime > _jumpTime / 2 && IsGrounded == true)
                    break;

                float normalizedJumpTime = Mathf.Clamp01(currentJumpTime / _jumpTime);
                float jumpAmount = _jumpCurve.Evaluate(normalizedJumpTime) * _jumpHeight;
                
                Vector3 newPosition = transform.position;
                newPosition.y = heightOnStartJump + jumpAmount;
                transform.position = newPosition;
                yield return null;
            }
        }

        _jumpRoutine = null;
        _isJump = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _groundDistance);
    }
}

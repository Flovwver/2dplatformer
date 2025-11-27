using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Vector2 _offsetBottomLeft;
    [SerializeField] private Vector2 _offsetTopRight;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _coyoteTime = 0.3f;

    private float _coyoteTimeCounter;

    private void Update()
    {
        if (IsGroundedWithoutCoyote())
            _coyoteTimeCounter = _coyoteTime;
        else if (_coyoteTimeCounter - Time.deltaTime > 0f)
            _coyoteTimeCounter -= Time.deltaTime;
        else
            _coyoteTimeCounter = 0f;
    }

    public bool IsGrounded()
    {
        return _coyoteTimeCounter > 0f || IsGroundedWithoutCoyote();
    }

    private bool IsGroundedWithoutCoyote()
    {
        Vector2 bottomLeft = (Vector2)transform.position + _offsetBottomLeft;
        Vector2 topRight = (Vector2)transform.position + _offsetTopRight;

        return Physics2D.OverlapArea(bottomLeft, topRight, _groundMask);
    }

    private void OnDrawGizmos()
    {
        Vector2 bottomLeft = (Vector2)transform.position + _offsetBottomLeft;
        Vector2 topRight = (Vector2)transform.position + _offsetTopRight;
        Vector2 size = topRight - bottomLeft;
        Vector2 center = bottomLeft + size * 0.5f;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
}

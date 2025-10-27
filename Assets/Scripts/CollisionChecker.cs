using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField] private Transform _leftGroundChecker;
    [SerializeField] private Transform _rightGroundChecker;
    [SerializeField] private float _groundDistance = 0.1f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _coyoteTime = 0.2f;

    private float _coyoteTimeCounter;

    public bool IsGrounded => _isGrounded || _coyoteTimeCounter > 0f;
    
    private bool _isGrounded
    {
        get
        {
            return Physics2D.Raycast(_leftGroundChecker.position, Vector2.down, _groundDistance, _groundMask)
                || Physics2D.Raycast(_rightGroundChecker.position, Vector2.down, _groundDistance, _groundMask);
        }
    }

    private void Update()
    {
        if (_isGrounded)
            _coyoteTimeCounter = _coyoteTime;
        else
            _coyoteTimeCounter -= Time.deltaTime;
    }
}

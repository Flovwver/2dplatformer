using System.Collections.Generic;
using UnityEngine;

public class DebudPainter : MonoBehaviour
{
    [SerializeField] private float maxTrailLength = 2f;

    [SerializeField] private Transform _leftGroundChecker;
    [SerializeField] private Transform _rightGroundChecker;
    [SerializeField] private float _groundDistance = 0.3f;
    [SerializeField] private LayerMask _groundMask;

    private readonly List<Vector3> _positions = new();
    private readonly List<float> _timestamps = new();

    void Update()
    {
        _positions.Add(transform.position);
        _timestamps.Add(Time.time);

        while (_timestamps.Count > 0 && Time.time - _timestamps[0] > maxTrailLength)
        {
            _timestamps.RemoveAt(0);
            _positions.RemoveAt(0);
        }

        for (int i = 1; i < _positions.Count; i++)
        {
            Debug.DrawLine(_positions[i - 1], _positions[i], Color.yellow);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (_leftGroundChecker == null || _rightGroundChecker == null)
            return;

        bool leftHit = Physics2D.Raycast(_leftGroundChecker.position, Vector2.down, _groundDistance, _groundMask);
        bool rightHit = Physics2D.Raycast(_rightGroundChecker.position, Vector2.down, _groundDistance, _groundMask);

        Gizmos.color = leftHit ? Color.green : Color.red;
        Gizmos.DrawLine(_leftGroundChecker.position, _leftGroundChecker.position + Vector3.down * _groundDistance);
        Gizmos.DrawWireSphere(_leftGroundChecker.position + Vector3.down * _groundDistance, 0.02f);

        Gizmos.color = rightHit ? Color.green : Color.red;
        Gizmos.DrawLine(_rightGroundChecker.position, _rightGroundChecker.position + Vector3.down * _groundDistance);
        Gizmos.DrawWireSphere(_rightGroundChecker.position + Vector3.down * _groundDistance, 0.02f);
        
    }
}

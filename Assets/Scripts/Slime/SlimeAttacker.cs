using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SlimeAttacker : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private LayerMask _targetLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _targetLayer.value) != 0
            && collision.gameObject.TryGetComponent<Health>(out var targetHealth))
        {
            targetHealth.TakeDamage(_damage, transform.position);
        }
    }
}

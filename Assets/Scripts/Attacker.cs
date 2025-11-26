using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Collider2D _attackCollider;
    [SerializeField] private ContactFilter2D _filter;

    private readonly Collider2D[] _hits = new Collider2D[16];

    public void Attack()
    {
        int count = Physics2D.OverlapCollider(_attackCollider, _filter, _hits);

        for (int i = 0; i < count; i++)
        {
            if (_hits[i].TryGetComponent(out Health health))
                health.TakeDamage(_damage, transform.position);
        }
    }
}

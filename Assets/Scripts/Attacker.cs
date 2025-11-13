using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Attacker : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _attackTime = 0.1f;

    private Coroutine _attackCoroutine;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _targetLayer 
            && collision.gameObject.TryGetComponent<Health>(out var targetHealth))
        {
            targetHealth.TakeDamage(_damage);
        }
    }

    public void StartAttack()
    {
        _attackCoroutine ??= StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;

        yield return new WaitForSeconds(_attackTime);

        _spriteRenderer.enabled = false;
        _collider.enabled = false;

        _attackCoroutine = null;
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _damageForce;
    [SerializeField] private Vector2 _damageSource;

    private bool _isDamaged = false;

    private Rigidbody2D _rigidbody;

    public event Action Died;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isDamaged)
        {
            Vector3 recoilAngel = new(0f, 0f, 45f);
            Vector2 rawDirection = (Vector2)transform.position - _damageSource;
            Vector2 horizontalDirection = new(Mathf.Sign(rawDirection.x), 0f); 

            Vector2 recoilDirection = Quaternion.Euler(Mathf.Sign(rawDirection.x) * recoilAngel) * horizontalDirection;

            _rigidbody.linearVelocity += recoilDirection * _damageForce;

            _isDamaged = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<FirstAidKit>(out var firstAidKit))
        {
            if (_currentHealth < _maxHealth)
            {
                Destroy(collider.gameObject);
                _currentHealth += firstAidKit.HealAmount;
            }
        }
    }

    public void TakeDamage(int damage, Vector2 damageSource)
    {
        if (damage < 0)
            return;

        if (_currentHealth - damage >= 0)
            _currentHealth -= damage;
        else
            _currentHealth = 0;

        if (_currentHealth <= 0)
            Died?.Invoke();

        _isDamaged = true;

        _damageSource = damageSource;
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    public event Action Died;
    public event Action<Vector2> Damaged;

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

        Damaged?.Invoke(damageSource);
    }
}

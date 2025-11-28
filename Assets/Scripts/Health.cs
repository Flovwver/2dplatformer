using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    public event Action Died;
    public event Action<Vector2> Damaged;

    public bool TryHeal(int healAmount)
    {
        if (_currentHealth < _maxHealth)
        {
            if (_currentHealth + healAmount > _maxHealth)
                _currentHealth = _maxHealth;
            else
                _currentHealth += healAmount;
        }

        return _currentHealth < _maxHealth;
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

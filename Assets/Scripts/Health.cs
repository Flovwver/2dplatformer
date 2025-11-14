using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    public event Action Died;

    public void TakeDamage(int damage)
    {

        if (damage < 0)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Died?.Invoke();
    }
}

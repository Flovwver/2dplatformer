using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private bool _isAlive = true;

    public event Action Died;
    public event Action<Vector2> Damaged;
    public event Action<int, int> ValueChanged;

    public void TakeHeal(int healAmount)
    {
        if (healAmount < 0)
            return;

        Add(healAmount);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            return;

        Add(-damage);

        if (_currentValue <= 0)
            Died?.Invoke();
    }

    private void Add(int value)
    {
        if (!_isAlive)
            return;

        float _lastValue = _currentValue;

        _currentValue += value;

        _currentValue = Mathf.Clamp(_currentValue, 0, _maxValue);

        if (_lastValue != _currentValue)
            ValueChanged?.Invoke(_currentValue, _maxValue);

        if (_currentValue <= 0)
            _isAlive = false;
    }
}

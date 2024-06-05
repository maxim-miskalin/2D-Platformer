using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action ChangeValue;

    [SerializeField, Min(0)] private float _maxValue;
    [SerializeField] private float _currentValue;

    public float CurrentValue => _currentValue;

    private void Awake()
    {
        _currentValue = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        _currentValue -= damage;

        if (_currentValue < 0)
            _currentValue = 0;

        ChangeValue?.Invoke();
    }

    public void RestoreHealth(float health)
    {
        _currentValue += health;

        if(_currentValue > _maxValue)
            _currentValue = _maxValue;
    }
}

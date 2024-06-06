using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private float _maxValue;
    [SerializeField] private float _currentValue;

    public float CurrentValue => _currentValue;

    public event Action ÑhangedValue;

    private void Awake()
    {
        _currentValue = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        _currentValue -= damage;
        _currentValue = Math.Clamp(_currentValue, 0, _maxValue);

        ÑhangedValue?.Invoke();
    }

    public void RestoreHealth(float health)
    {
        _currentValue += health;
        _currentValue = Math.Clamp(_currentValue, 0, _maxValue);
    }
}

using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField, Min(0)] private float _value;

    public void ReduceHealth(Health health)
    {
        health.TakeDamage(_value);
    }
}

using UnityEngine;

[RequireComponent (typeof(Health))]
public class Heart : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthKit kit))
        {
            _health.RestoreHealth(kit.ToDestroyed());
        }
    }
}

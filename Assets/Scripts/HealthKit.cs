using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private float _healthValue = 25f;
    [SerializeField] private float _timeOfDestruction = 1f;

    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new(_timeOfDestruction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MoverPlayer player))
        {
            if (player.TryGetComponent(out Health health))
            {
                health.RestoreHealth(_healthValue);
                Destroy(gameObject);
            }
        }
    }
}

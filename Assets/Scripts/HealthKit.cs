using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private float _healthValue = 25f;

    public float ToDestroyed()
    {
        Destroy(gameObject);
        return _healthValue;
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TargetDetection))]
public class AttakSlime : MonoBehaviour
{
    [SerializeField] private float _distance = 0.5f;
    [SerializeField] private float _frequency = 1f;
    [SerializeField, Min(0)] private float _damage;

    private TargetDetection _targetDetection;
    private Transform _target;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Awake()
    {
        _targetDetection = GetComponent<TargetDetection>();
    }

    private void OnEnable()
    {
        _targetDetection.Locate += SetTarget;
    }

    private void OnDisable()
    {
        _targetDetection.Locate -= SetTarget;
    }

    private void Start()
    {
        _wait = new(_frequency);
    }

    private void SetTarget(Collider2D target)
    {
        if (target != null)
        {
            if (target != _target)
            {
                _target = target.transform;
                _coroutine = StartCoroutine(ReduceHealth());
            }
        }
        else
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _target = null;
        }
    }

    private IEnumerator ReduceHealth()
    {
        if (_target.TryGetComponent(out Health health))
        {
            while (health.CurrentValue > 0)
            {
                if (Vector2.Distance(transform.position, _target.position) <= _distance)
                    health.TakeDamage(_damage);

                yield return _wait;
            }
        }
    }
}

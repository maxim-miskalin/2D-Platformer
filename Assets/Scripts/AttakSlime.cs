using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TargetDetection), typeof(Damage))]
public class AttakSlime : MonoBehaviour
{
    [SerializeField] private float _distance = 0.5f;
    [SerializeField] private float _frequency = 1f;

    private TargetDetection _targetDetection;
    private Transform _target;
    private Damage _damage;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Awake()
    {
        _targetDetection = GetComponent<TargetDetection>();
        _damage = GetComponent<Damage>();
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
                    _damage.ReduceHealth(health);

                yield return _wait;
            }
        }
    }
}

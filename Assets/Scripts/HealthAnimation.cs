using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class HealthAnimation : MonoBehaviour
{
    [SerializeField] private float _delayOfDisappearance = 5f;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private Health _health;

    private MoverPlayer _mover;
    private MoverSlime _mobMover;

    private Color _defaultColor;
    private Color _deathColor;
    private float _deathColorAlfa = 0.5f;
    private WaitForSeconds _waitDie;
    private WaitForSeconds _waitColor;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();

        _defaultColor = _renderer.color;
    }

    private void OnEnable()
    {
        _health.ÑhangedValue += CheckHealth;
    }

    private void OnDisable()
    {
        _health.ÑhangedValue -= CheckHealth;
    }

    private void Start()
    {
        if (TryGetComponent(out MoverPlayer mover))
            _mover = mover;
        else if (TryGetComponent(out MoverSlime _mover))
            _mobMover = _mover;

        _waitDie = new(_delayOfDisappearance);
        _waitColor = new(0.1f);
        _deathColor = _renderer.color;
        _deathColor.a = _deathColorAlfa;
    }

    private void CheckHealth()
    {
        if (_health.CurrentValue <= 0)
            StartCoroutine(Die());
        else
            StartCoroutine(ChangeColor());
    }

    private IEnumerator Die()
    {
        if (_mover != null)
            _mover.enabled = false;
        else if (_mobMover != null)
            _mobMover.enabled = false;

        _animator.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, 90f);
        _renderer.color = _deathColor;
        yield return _waitDie;

        _rigidbody.simulated = false;
        _collider.enabled = false;
        yield return _waitDie;

        Destroy(gameObject);
    }

    private IEnumerator ChangeColor()
    {
        _renderer.color = Color.red;
        yield return _waitColor;
        _renderer.color = _defaultColor;
    }
}

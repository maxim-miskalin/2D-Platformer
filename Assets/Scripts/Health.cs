using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Health : MonoBehaviour
{
    public event Action<float> ChangeValue;

    [SerializeField, Min(0)] private float _maxValue;
    [SerializeField] private float _delayOfDisappearance = 5f;
    [SerializeField] private float _currentValue;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private Animator _animator;

    private MoverPlayer _mover;
    private MoverSlime _mobMover;

    private Color _defaultColor;
    private Color _deathColor;
    private float _deathColorAlfa = 0.5f;
    private WaitForSeconds _wait;

    public float CurrentValue => _currentValue;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _currentValue = _maxValue;
        _defaultColor = _renderer.color;
    }

    private void Start()
    {
        if (TryGetComponent(out MoverPlayer mover))
            _mover = mover;
        else if (TryGetComponent(out MoverSlime _mover))
            _mobMover = _mover;

        _wait = new(_delayOfDisappearance);
        _deathColor = _renderer.color;
        _deathColor.a = _deathColorAlfa;
    }

    private void Update()
    {
        if (_currentValue <= 0)
            StartCoroutine(Die());
    }

    public void TakeDamage(float damage)
    {
        _currentValue -= damage;

        if (_currentValue < 0)
            _currentValue = 0;

        ChangeValue?.Invoke(_currentValue);

        Debug.Log($"{gameObject.name} нанесено {damage} урона");

        StartCoroutine(ChangeColor());
    }

    public void RestoreHealth(float health)
    {
        _currentValue += health;

        if(_currentValue > _maxValue)
            _currentValue = _maxValue;
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
        yield return _wait;

        _rigidbody.simulated = false;
        _collider.enabled = false;
        yield return _wait;

        Destroy(gameObject);
    }

    private IEnumerator ChangeColor()
    {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _renderer.color = _defaultColor;
    }
}

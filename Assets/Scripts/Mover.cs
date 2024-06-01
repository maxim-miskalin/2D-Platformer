using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _sittingSize = 0.7f;
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _jumpPower = 8f;
    [SerializeField] private float _fallGravityScale = 2.5f;

    private string _groundTag = "Ground";
    private string _horizontalInput = "Horizontal";
    private string _jumpInput = "Jump";
    private string _animationMoveX = "MoveX";

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private float _defualtGravityScale;
    private float _defualtScaleY;
    private bool _isGround = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _defualtGravityScale = _rigidbody.gravityScale;
        _defualtScaleY = transform.localScale.y;
    }

    private void FixedUpdate()
    {
        SetGravity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(_groundTag))
            _isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(_groundTag))
            _isGround = false;
    }

    private void Update()
    {
        transform.Translate(new(Input.GetAxis(_horizontalInput) * _speed * Time.deltaTime, 0));

        if (Input.GetButtonDown(_jumpInput) && _isGround)
            _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

        if (Input.GetKey(KeyCode.LeftShift))
            transform.localScale = new(transform.localScale.x, _sittingSize);
        else
            transform.localScale = new(transform.localScale.x, _defualtScaleY);

        _animator.SetFloat(_animationMoveX, Mathf.Abs(Input.GetAxis(_horizontalInput)));

        TurnToSide();
    }

    private void SetGravity()
    {
        if (_rigidbody.velocity.y < 0 && !_isGround)
            _rigidbody.gravityScale = _fallGravityScale;
        else
            _rigidbody.gravityScale = _defualtGravityScale;
    }

    private void TurnToSide()
    {
        if (Input.GetAxis(_horizontalInput) < 0)
            _spriteRenderer.flipX = true;
        else if (Input.GetAxis(_horizontalInput) > 0)
            _spriteRenderer.flipX = false;
    }
}

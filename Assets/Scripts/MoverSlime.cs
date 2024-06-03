using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TargetDetection))]
public class MoverSlime : MonoBehaviour
{
    [SerializeField] private Transform _wayPoint;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpPower = 1.5f;
    [SerializeField] private float _minDistantionToPoint = 0.5f;

    private TargetDetection _targetDetection;
    private Rigidbody2D _rigidbody2D;
    private List<Transform> _way = new();
    private Transform _target;
    private int _index = 0;
    private bool _isGround = false;
    private string _groundTag = "Ground";

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _targetDetection = GetComponent<TargetDetection>();
    }

    private void OnEnable()
    {
        _targetDetection.Locate += MovingTowardsGoal;
    }

    private void OnDisable()
    {
        _targetDetection.Locate -= MovingTowardsGoal;
    }

    private void Start()
    {
        for (int i = 0; i < _wayPoint.childCount; i++)
            _way.Add(_wayPoint.GetChild(i));
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
        if (Vector2.Distance(transform.position, _target.position) > _minDistantionToPoint)
        {
            if (_isGround)
                Jump();

            if (_isGround == false)
                Move();
        }
        else
        {
            _index = ++_index % _way.Count;
        }
    }

    private void MovingTowardsGoal(Collider2D collider)
    {
        if (collider != null)
            _target = collider.transform;
        else 
            _target = _way[_index];
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }
}

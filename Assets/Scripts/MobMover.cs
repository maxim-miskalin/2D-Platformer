using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MobMover : MonoBehaviour
{
    [SerializeField] private Transform _wayPoint;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpPower = 0.5f;
    [SerializeField] private float _minDistantionToPoint = 0.5f;

    private Rigidbody2D _rigidbody2D;
    private List<Transform> _way = new();
    private Transform _point;
    private int _index = 0;
    private bool _isGround = false;
    private string _groundTag = "Ground";

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

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
        _point = _way[_index];

        if (Vector2.Distance(transform.position, _point.position) > _minDistantionToPoint)
        {
            if (_isGround)
                _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

            if (_isGround == false)
                transform.position = Vector2.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);
        }
        else
        {
            _index = ++_index % _way.Count;
        }
    }
}

using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _playerTarget;
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private Vector3 _offset = new(0, 0, 10f);

    private void Update()
    {
        if (_playerTarget != null)
        {
            Vector3 desiredPosition = _playerTarget.position - _offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, -_offset, _smoothSpeed * Time.deltaTime);
        }
    }
}
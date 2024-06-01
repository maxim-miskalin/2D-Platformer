using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _playerTarget;
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private Vector3 _offset = new(0, 0, 10f);

    private void Update()
    {
        Vector3 desiredPosition = _playerTarget.position - _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
    }
}
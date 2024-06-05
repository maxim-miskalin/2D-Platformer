using UnityEngine;

public class AttakPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _distance = 10f;
    [SerializeField, Min(0)] private float _damage;

    private Vector2 _mousePosition;

    private void Update()
    {
        _mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Health health = FindTarget();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }
        }
    }

    private Health FindTarget()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, _mousePosition, _distance, _enemyLayer);

        if (raycastHit.collider != null && raycastHit.collider.TryGetComponent(out Health health))
            return health;

        return null;
    }
}

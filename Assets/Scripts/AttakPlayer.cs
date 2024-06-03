using UnityEngine;

[RequireComponent(typeof(Damage))]
public class AttakPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _distance = 10f;

    private Damage _damage;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _damage = GetComponent<Damage>();
    }

    private void FixedUpdate()
    {
        _mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Health health = HitTarget();

            if (health != null)
            {
                _damage.ReduceHealth(health);
            }
        }
    }

    private Health HitTarget()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, _mousePosition, _distance, _enemyLayer);

        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.TryGetComponent(out Health health))
                return health;
            else
                return null;
        }
        else
        {
            return null;
        }
    }
}

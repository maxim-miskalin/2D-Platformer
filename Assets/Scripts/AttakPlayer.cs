using UnityEngine;

[RequireComponent(typeof(MoverPlayer))]
public class AttakPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _distance = 10f;
    [SerializeField, Min(0)] private float _damage;

    private MoverPlayer _player;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _player = GetComponent<MoverPlayer>();
    }

    private void OnEnable()
    {
        _player.Attacked += Attack;
    }

    private void OnDisable()
    {
        _player.Attacked -= Attack;
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;
    }

    private void Attack()
    {
        Health health = FindTarget();

        if (health != null)
        {
            health.TakeDamage(_damage);
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

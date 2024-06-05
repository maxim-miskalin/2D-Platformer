using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetDetection : MonoBehaviour
{
    public event Action<Collider2D> Locate;

    [SerializeField] private float _detectionRadius = 6f;
    [SerializeField] private LayerMask _playerLayer;

    private Collider2D _target;

    private void Update()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, _detectionRadius, _playerLayer);

        if (CheckForHealth(target))
        {
            if (target != _target)
            {
                _target = target;

                if (target.TryGetComponent(out MoverPlayer player))
                    Locate?.Invoke(target);
            }
        }
        else
        {
            _target = null;
            Locate?.Invoke(null);
        }
    }

    private bool CheckForHealth(Collider2D target)
    {
        if (target != null && target.TryGetComponent(out Health health))
        {
            if (health.CurrentValue > 0)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }
}
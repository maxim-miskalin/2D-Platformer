using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    public event Action<Coin> Disappear;

    public void ToDestroyed()
    {
        Disappear?.Invoke(this);
        Destroy(gameObject);
    }
}

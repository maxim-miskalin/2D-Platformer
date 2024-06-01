using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    public event Action Disappear;

    public void ToDestroyed()
    {
        Disappear?.Invoke();
        gameObject.SetActive(false);
    }
}

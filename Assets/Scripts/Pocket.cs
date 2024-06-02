using UnityEngine;

public class Pocket : MonoBehaviour
{
    private int _coinsCounter = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.ToDestroyed();
            _coinsCounter++;
        }
    }
}

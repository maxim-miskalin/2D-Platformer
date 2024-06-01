using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefabCoin;
    [SerializeField] private float _spawnTime = 5f;

    private Coin _coin;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new(0);

        StartCoroutine(CreateCoin());

        _wait = new(_spawnTime);
    }

    private void DestroyCoin()
    {
        _coin.Disappear -= DestroyCoin;
        Destroy(_coin.gameObject);
        StartCoroutine(CreateCoin());
    }

    private IEnumerator CreateCoin()
    {
        yield return _wait;

        _coin = Instantiate(_prefabCoin, transform);
        _coin.Disappear += DestroyCoin;
    }
}

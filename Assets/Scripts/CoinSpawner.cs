using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefabCoin;
    [SerializeField] private float _spawnTime = 5f;

    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new(0);

        StartCoroutine(CreateCoin());

        _wait = new(_spawnTime);
    }

    private void CreateNewCoin()
    {
        StartCoroutine(CreateCoin());
    }

    private IEnumerator CreateCoin()
    {
        yield return _wait;

        Coin coin = Instantiate(_prefabCoin, transform);
        coin.Disappear += CreateNewCoin;
    }
}

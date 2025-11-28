using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private bool _isAllowSpawn = true;

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (_isAllowSpawn)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        Instantiate(_coinPrefab, spawnPoint.position, Quaternion.identity);
    }
}

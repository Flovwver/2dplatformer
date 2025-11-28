using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FirstAidKitSpawner : MonoBehaviour
{
    [SerializeField] private FirstAidKit _firstAidKitPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private bool isAllowSpawn = true;

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (isAllowSpawn)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        Instantiate(_firstAidKitPrefab, spawnPoint.position, Quaternion.identity);
    }
}

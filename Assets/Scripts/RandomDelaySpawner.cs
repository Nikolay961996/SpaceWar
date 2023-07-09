using System.Collections;
using UnityEngine;

public class RandomDelaySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _maxSpawnDelay;

    private void Start()
    {
        StartCoroutine(nameof(DelaySpawn));
    }
    
    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
        Spawn();
        StartCoroutine(nameof(DelaySpawn));
    }

    private void Spawn()
    {
        Instantiate(_prefab, transform.position, transform.rotation);
    }
}

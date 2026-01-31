using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject meleeEnemyPrefab;
    [SerializeField] GameObject rangeEnemyPrefab;
    [SerializeField] float rangeEnemySpawnProbability;
    [SerializeField] float spawnWait;
    [SerializeField] float spawnDelay;

    private void Start()
    {
        InvokeRepeating("spawnEnemy", spawnDelay, spawnWait);
    }

    void spawnEnemy()
    {
        // if (Random.value < rangeEnemySpawnProbability)
        // {
            GameServices.Get<Pool>().Spawn(meleeEnemyPrefab, out GameObject spawnedEnemy);
            spawnedEnemy.transform.position = transform.position;
        // }
        // else
        // {
        //     //GameServices.Get<Pool>().Spawn(rangeEnemyPrefab,  out GameObject spawnedRange);
        //     //spawnedRange.transform.position = transform.position;
        // }
    }

    private void OnDestroy()
    {
        CancelInvoke("spawnEnemy");
    }
}

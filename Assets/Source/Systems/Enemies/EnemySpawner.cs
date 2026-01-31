using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnWait;
    [SerializeField] float spawnDelay;

    private void Start()
    {
        InvokeRepeating("spawnEnemy", spawnDelay, spawnWait);
    }

    void spawnEnemy()
    {
        GameServices.Get<Pool>().Spawn(enemyPrefab, out GameObject spawnedEnemy);
        spawnedEnemy.transform.position = transform.position;
    }

    private void OnDestroy()
    {
        CancelInvoke("spawnEnemy");
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BlackHole : MonoBehaviour
{
    public enum SurfaceType
    {
        Floor,
        Ceiling,
        Wall
    }
    
    [SerializeField] private SurfaceType surfaceType;
    [SerializeField] GameObject meleeEnemyPrefab;
    [SerializeField] GameObject rangeEnemyPrefab;
    [SerializeField] float rangeEnemySpawnProbability;
    [SerializeField] float spawnWait;
    [SerializeField] float spawnDelay;
    [SerializeField] private bool waiting;

    private Pool pool;
    
    private float spawnDelayTimer = 0;
    private float spawnTimer = 0;
    
    private void Awake()
    {
        pool = GameServices.Get<Pool>();
        waiting = true;
    }

    private List<EnemyHealth> spawnedEnemies = new List<EnemyHealth>();
    
    void SpawnEnemy()
    {
        if (UnityEngine.Random.value > rangeEnemySpawnProbability)
        {
            pool.Spawn(meleeEnemyPrefab, out GameObject spawnedEnemy);
            spawnedEnemy.GetComponent<NavMeshAgent>().Warp(transform.position);
            EnemyHealth enemyHealth = spawnedEnemy.GetComponentInChildren<EnemyHealth>();
            spawnedEnemies.Add(enemyHealth);
        }
        else
        { 
            pool.Spawn(rangeEnemyPrefab,  out GameObject spawnedRange);
            spawnedRange.transform.position = transform.position;
            EnemyHealth enemyHealth = spawnedRange.GetComponentInChildren<EnemyHealth>();
            spawnedEnemies.Add(enemyHealth);
        }
    }

    public void KillEnemies()
    {
        foreach (EnemyHealth spawnedEnemy in spawnedEnemies)
        {
            DamageMessage message = new();
            message.value = 10000;
            if(spawnedEnemy.gameObject.activeInHierarchy)
                spawnedEnemy.TakeDamage(message);
        }
        spawnedEnemies.Clear();
    }

    private void Update()
    {
        if (waiting)
        {
            spawnDelayTimer += Time.deltaTime;
            if (spawnDelayTimer > spawnDelay)
            {
                waiting = false;
                spawnDelayTimer = 0;
            }
        }
        else
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnWait)
            {
                SpawnEnemy();
                spawnTimer = 0;
            }
        }
    }

    public SurfaceType CurrentSurfaceType => surfaceType;
}

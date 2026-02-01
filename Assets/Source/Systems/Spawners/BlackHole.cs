using System;
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

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnDelay, spawnWait);
    }

    void SpawnEnemy()
    {
        if (UnityEngine.Random.value > rangeEnemySpawnProbability)
        {
            pool.Spawn(meleeEnemyPrefab, out GameObject spawnedEnemy);
            spawnedEnemy.GetComponent<NavMeshAgent>().Warp(transform.position);
        }
        else
        { 
            GameServices.Get<Pool>().Spawn(rangeEnemyPrefab,  out GameObject spawnedRange);
            spawnedRange.transform.position = transform.position;
        }
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

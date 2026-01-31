using System;
using UnityEngine;
using UnityEngine.AI;

public class BlackHole : MonoBehaviour
{
    public enum SurfaceType
    {
        Floor,
        Ceiling,
        Wall
    }
    
    [SerializeField] private int roomId;
    [SerializeField] private SurfaceType surfaceType;
    [SerializeField] GameObject meleeEnemyPrefab;
    [SerializeField] GameObject rangeEnemyPrefab;
    [SerializeField] float rangeEnemySpawnProbability;
    [SerializeField] float spawnWait;
    [SerializeField] float spawnDelay;

    private void Awake()
    {
        GameServices.Get<BlackHoleRegistry>().Register(roomId, this);
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnDelay, spawnWait);
    }

    void SpawnEnemy()
    {
        // if (Random.value < rangeEnemySpawnProbability)
        // {
            GameServices.Get<Pool>().Spawn(meleeEnemyPrefab, out GameObject spawnedEnemy);
            spawnedEnemy.GetComponent<NavMeshAgent>().Warp(transform.position);
        // }
        // else
        // {
        //     //GameServices.Get<Pool>().Spawn(rangeEnemyPrefab,  out GameObject spawnedRange);
        //     //spawnedRange.transform.position = transform.position;
        // }
    }

    private void OnDestroy()
    {
        CancelInvoke("SpawnEnemy");
        GameServices.Get<BlackHoleRegistry>().Unregister(roomId, this);
    }

    public SurfaceType CurrentSurfaceType => surfaceType;
}

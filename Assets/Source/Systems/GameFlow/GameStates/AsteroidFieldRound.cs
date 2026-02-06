using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AsteroidFieldRound : GameStage
{
    private const string CONFIG_FILE_PATH = "GameData/AsteroidFieldRoundConfig";
    private const float ROUND_TIME = 120.0f;
    
    private static readonly Dictionary<uint, WaveSpawnerMetric> spawnerTable = new Dictionary<uint, WaveSpawnerMetric>()
    {
        {0, new WaveSpawnerMetric( 2, 2)},
        {3, new WaveSpawnerMetric(4, 2)},
        {38, new WaveSpawnerMetric(6, 2)},
    };

    private AsteroidFieldRoundConfig config;
    
    private BlackHoleRegistry registry;
    private int totalActiveSpawners;
    private float timer;
    private List<BlackHoleReference> blackHoles = new List<BlackHoleReference>();
    

    private GameFlow flow;
    
    private void Awake()
    {
        config = Resources.Load<AsteroidFieldRoundConfig>(CONFIG_FILE_PATH);
        registry = GameServices.Get<BlackHoleRegistry>();
        flow = GameServices.Get<GameFlow>();
    }

    void SpawnRandomBuff()
    {
        GameObject newBuff = config.postRoundBuffPrefabs[Random.Range(0, config.postRoundBuffPrefabs.Length)];
        Vector3 playerPosition = GameServices.Get<Player>().CharacterPosition;
        Vector2 randomNearbyPosition2D = Random.insideUnitCircle * Random.Range(config.minRadius, config.maxRadius);
        Vector3 randomNearbyPosition3D = playerPosition + new Vector3(randomNearbyPosition2D.x, 0, randomNearbyPosition2D.y);
        if (NavMesh.SamplePosition(randomNearbyPosition3D, out NavMeshHit hit, config.maxRadius * 2.0f, NavMesh.AllAreas))
        {
            Debug.Log("Spawning on navmesh");
            randomNearbyPosition3D = hit.position;
        }
        GameServices.Get<Pool>().Spawn(newBuff, out GameObject newBuffInstance, TransformFrame.T(randomNearbyPosition3D + Vector3.up * config.spawnHeight));
    }
    
    public void HandleSpawnerRepair()
    {
        totalActiveSpawners--;
        if (totalActiveSpawners <= 0)
        {
            SpawnRandomBuff();
            flow.SwitchStage(GameStageType.EnemyWave);
        }
    }
    
    WaveSpawnerMetric PickSpawnerMetric()
    {
        uint currentRound = GameServices.Get<Game>().currentRound;
        for (int i = 0; i < currentRound; i++)
        {
            foreach (KeyValuePair<uint,WaveSpawnerMetric> spawnerMetric in spawnerTable)
            {
                if (spawnerMetric.Key >= i)
                {
                    return spawnerMetric.Value;
                }
            }
        }
        return spawnerTable.Last().Value;
    }
    
    void TurnOnSpawners()
    {
        WaveSpawnerMetric metric = PickSpawnerMetric();
        totalActiveSpawners = 0;
        List<int> orderedRooms = GameServices.Get<RoomRegistry>()
            .SortByDistance(GameServices.Get<Player>().CharacterPosition);
        foreach (var roomId in orderedRooms)
        {
            if (totalActiveSpawners > metric.totalSpawners) break;
            List<BlackHoleReference> blackHoles = registry.GetBlackHolesByRoom(roomId);
            for (int blackHoleRefId = 0; blackHoleRefId < Random.Range(Mathf.CeilToInt(metric.roomDensity * 0.5f), metric.roomDensity); blackHoleRefId++)
            {
                if (blackHoleRefId > blackHoles.Count - 1) break;
                BlackHoleReference blackHoleRef = blackHoles[blackHoleRefId];
                blackHoleRef.Activate(false);
                blackHoles.Add(blackHoleRef);
                totalActiveSpawners++;
            }
        }
    }

    void TurnOffSpawners()
    {
        foreach (BlackHoleReference blackHoleReference in blackHoles)
        {
            blackHoleReference.Deactivate();
        }
    }

    public override void OnStateEnter()
    {
        timer = 0;
        SpawnRandomBuff();
        blackHoles.Clear();
        TurnOnSpawners();
    }

    public override void OnStateExit()
    {
        timer = 0;
        blackHoles.Clear();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= ROUND_TIME)
        {
            timer = 0;
            TurnOffSpawners();
            flow.SwitchStage(GameStageType.EnemyWave);
        }
    }
}

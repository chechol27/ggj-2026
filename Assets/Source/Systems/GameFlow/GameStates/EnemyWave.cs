using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public struct WaveSpawnerMetric
{
    public int totalSpawners;
    public int roomDensity;

    public WaveSpawnerMetric(int totalSpawners, int roomDensity)
    {
        this.totalSpawners = totalSpawners;
        this.roomDensity = roomDensity;
    }
}

public class EnemyWave : GameStage
{
    private static readonly Dictionary<uint, WaveSpawnerMetric> spawnerTable = new Dictionary<uint, WaveSpawnerMetric>()
    {
        {0, new WaveSpawnerMetric( 10, 1)},
        {3, new WaveSpawnerMetric(2, 1)},
        {8, new WaveSpawnerMetric(3, 1)},
        {18, new WaveSpawnerMetric(5, 2)},
        {28, new WaveSpawnerMetric(7, 3)},
        {38, new WaveSpawnerMetric(10, 2)},
    };

    [SerializeField] private int totalActiveSpawners;

    private GameFlow flow;
    private Game game;
    private void Awake()
    {
        flow = GameServices.Get<GameFlow>();
        game = GameServices.Get<Game>();
    }

    WaveSpawnerMetric PickSpawnerMetric()
    {
        uint currentRound = GameServices.Get<Game>().currentRound;
        for (int i = -1; i < currentRound; i++)
        {
            foreach (KeyValuePair<uint,WaveSpawnerMetric> spawnerMetric in spawnerTable)
            {
                if (spawnerMetric.Key >= i)
                {
                    return spawnerMetric.Value;
                }
            }
        }
        var ret = spawnerTable.Last().Value;

        return ret;
    }
    
    public void HandleSpawnerRepair()
    {
        totalActiveSpawners--;
        if (totalActiveSpawners <= 0)
        {
            flow.SwitchStage(GameStageType.Interlude);
            game.currentRound++;
        }
    }

    void TurnOnSpawners()
    {
        WaveSpawnerMetric metric = PickSpawnerMetric();
        BlackHoleRegistry blackHoleRegistry = GameServices.Get<BlackHoleRegistry>();
        totalActiveSpawners = 0;
        List<int> orderedRooms = GameServices.Get<RoomRegistry>()
            .SortByDistance(GameServices.Get<Player>().CharacterPosition);
        foreach (var roomId in orderedRooms)
        {
            if (totalActiveSpawners > metric.totalSpawners) break;
            List<BlackHoleReference> blackHoles = blackHoleRegistry.GetBlackHolesByRoom(roomId);
            for (int blackHoleRefId = 0; blackHoleRefId < Random.Range(Mathf.CeilToInt(metric.roomDensity * 0.5f), metric.roomDensity); blackHoleRefId++)
            {
                if (blackHoleRefId > blackHoles.Count - 1) break;
                BlackHoleReference blackHoleRef = blackHoles[blackHoleRefId];
                blackHoleRef.Activate();
                blackHoleRef.RegisterRepairListener(HandleSpawnerRepair);
                totalActiveSpawners++;
            }
        }
    }
    
    public override void OnStateEnter()
    {
        Debug.Log("Starting Wave");
        TurnOnSpawners();
    }

    public override void OnStateExit()
    {
        GameServices.Get<BlackHoleRegistry>().DisableBlackHoles();
    }
}

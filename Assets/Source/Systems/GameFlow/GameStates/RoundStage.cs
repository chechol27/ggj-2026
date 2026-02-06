using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct WaveSpawnerMetric
{
    public int minimumRound;
    public int totalSpawners;
    public int roomDensity;

    public WaveSpawnerMetric(int minimumRound, int totalSpawners, int roomDensity)
    {
        this.minimumRound = minimumRound;
        this.totalSpawners = totalSpawners;
        this.roomDensity = roomDensity;
    }
}

public abstract class RoundStage : GameStage
{
    private BlackHoleRegistry registry;
    protected GameFlow flow;
    protected Game game;

    [SerializeField] protected int totalActiveSpawners;
    protected List<BlackHoleReference> blackHoles = new List<BlackHoleReference>();

    public abstract void HandleSpawnerRepair();
    
    protected virtual void Awake()
    {
        flow = GameServices.Get<GameFlow>();
        game = GameServices.Get<Game>();
        registry = GameServices.Get<BlackHoleRegistry>();
    }
    
    private WaveSpawnerMetric PickSpawnerMetric(List<WaveSpawnerMetric> metrics)
    {
        uint currentRound = GameServices.Get<Game>().currentRound;
        for (int i = -1; i < currentRound; i++)
        {
            foreach (WaveSpawnerMetric spawnerMetric in metrics)
            {
                if (spawnerMetric.minimumRound >= i)
                {
                    return spawnerMetric;
                }
            }
        }
        var ret = metrics.Last();

        return ret;
    }
    
    protected void TurnOnSpawners(List<WaveSpawnerMetric> metrics, bool aggressive)
    {
        WaveSpawnerMetric metric = PickSpawnerMetric(metrics);
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
                blackHoleRef.Activate(aggressive);
                blackHoles.Add(blackHoleRef);
                totalActiveSpawners++;
            }
        }
    }
}

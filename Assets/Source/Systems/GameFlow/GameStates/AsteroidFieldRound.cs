using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidFieldRound : GameStage
{
    private static readonly Dictionary<uint, WaveSpawnerMetric> spawnerTable = new Dictionary<uint, WaveSpawnerMetric>()
    {
        {0, new WaveSpawnerMetric( 2, 2)},
        {3, new WaveSpawnerMetric(4, 2)},
        {38, new WaveSpawnerMetric(6, 2)},
    };
    
    private BlackHoleRegistry registry;
    private int totalActiveSpawners;

    private void Awake()
    {
        registry = GameServices.Get<BlackHoleRegistry>();
    }
    
    public void HandleSpawnerRepair()
    {
        totalActiveSpawners--;
        if (totalActiveSpawners <= 0)
        {
            //TODO Spawn Buff
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
                blackHoleRef.Activate(false);
                blackHoleRef.RegisterRepairListener(HandleSpawnerRepair, false);
                totalActiveSpawners++;
            }
        }
    }

    public override void OnStateEnter()
    {
        TurnOnSpawners();
    }
}

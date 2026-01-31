using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        {0, new WaveSpawnerMetric( 1, 1)},
        {3, new WaveSpawnerMetric(2, 1)},
        {8, new WaveSpawnerMetric(3, 1)},
        {18, new WaveSpawnerMetric(5, 2)},
        {28, new WaveSpawnerMetric(7, 3)},
        {38, new WaveSpawnerMetric(10, 2)},
    };

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
        int totalActivatedSpawners = 0;
        List<int> orderedRooms = GameServices.Get<RoomRegistry>()
            .SortByDistance(GameServices.Get<Player>().CharacterPosition);
        foreach (var roomId in orderedRooms)
        {
            if (totalActivatedSpawners > metric.totalSpawners) break;
            //Spawn spawners if density allws
            List<BlackHole> blackHoles = blackHoleRegistry.GetBlackHolesByRoom(roomId);
            for (int s = 0; s < Random.Range(Mathf.CeilToInt(metric.roomDensity * 0.5f), metric.roomDensity); s++)
            {
                if (s > blackHoles.Count - 1) break;
                blackHoles[s].gameObject.SetActive(true);
                totalActivatedSpawners++;
            }
        }
    }
    
    public override void OnStateEnter()
    {
        TurnOnSpawners();
    }

    public override void OnStateExit()
    {
        GameServices.Get<BlackHoleRegistry>().DisableBlackHoles();
    }
}

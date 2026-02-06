using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AsteroidFieldRound : RoundStage
{
    private const string CONFIG_FILE_PATH = "GameData/AsteroidFieldRoundConfig";
    private AsteroidFieldRoundConfig config;
    private float timer;
    
    protected override void Awake()
    {
        base.Awake();
        config = Resources.Load<AsteroidFieldRoundConfig>(CONFIG_FILE_PATH);
    }

    void SpawnRandomBuff()
    {
        GameObject newBuff = config.postRoundBuffPrefabs[Random.Range(0, config.postRoundBuffPrefabs.Length)];
        Vector3 playerPosition = GameServices.Get<Player>().CharacterPosition;
        Vector2 randomNearbyPosition2D = Random.insideUnitCircle * Random.Range(config.minRadius, config.maxRadius);
        Vector3 randomNearbyPosition3D = playerPosition + new Vector3(randomNearbyPosition2D.x, 0, randomNearbyPosition2D.y);
        if (NavMesh.SamplePosition(randomNearbyPosition3D, out NavMeshHit hit, config.maxRadius * 2.0f, NavMesh.AllAreas))
        {
            randomNearbyPosition3D = hit.position;
        }
        GameServices.Get<Pool>().Spawn(newBuff, out GameObject newBuffInstance, TransformFrame.T(randomNearbyPosition3D + Vector3.up * config.spawnHeight));
    }
    
    public override void HandleSpawnerRepair()
    {
        totalActiveSpawners--;
        if (totalActiveSpawners <= 0)
        {
            flow.SwitchStage(GameStageType.Interlude);
            SpawnRandomBuff();
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
        blackHoles.Clear();
        TurnOnSpawners(config.spawnersPerRound, false);
    }

    public override void OnStateExit()
    {
        timer = 0;
        blackHoles.Clear();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= config.timeLimit)
        {
            timer = 0;
            TurnOffSpawners();
            flow.SwitchStage(GameStageType.Interlude);
        }
    }
}

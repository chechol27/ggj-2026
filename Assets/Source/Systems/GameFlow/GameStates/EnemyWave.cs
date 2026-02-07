using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWave : RoundStage
{
    private const string CONFIG_FILE_PATH = "GameData/EnemyWaveRoundConfig";

    private EnemyWaveRoundConfig config;

    protected override void Awake()
    {
        base.Awake();
        config = Resources.Load<EnemyWaveRoundConfig>(CONFIG_FILE_PATH);
    }

    public override void HandleSpawnerRepair()
    {
        totalActiveSpawners--;
        if (totalActiveSpawners <= 0)
        {
            flow.SwitchStage(GameStageType.Interlude);
            game.currentRound++;
        }
    }
    
    public override void OnStateEnter()
    {
        Debug.Log("A");
        totalActiveSpawners = 0;
        TurnOnSpawners(config.spawnersPerRound, true);
    }

    public override void OnStateExit()
    {
        GameServices.Get<BlackHoleRegistry>().DisableBlackHoles();
    }
}

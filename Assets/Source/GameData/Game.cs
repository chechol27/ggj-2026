using System;
using UnityEngine;

public class Game : MonoBehaviour, IGameService
{
    public uint currentRound;
    public bool paused;

    private float counter = 1;

    private int points;
    public int Points
    {
        get => points;
        set => points = value;
    }

    private void Update()
    {
        if (GameServices.Get<GameFlow>().CurrentStageType != GameStageType.EnemyWave) return;
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            Points++;
            counter = 1;
        }
    }
}

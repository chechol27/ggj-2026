using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWaveRoundConfig", menuName = "Scriptable Objects/Game Stages/Enemy Wave Round Config")]

public class EnemyWaveRoundConfig : ScriptableObject
{
    public List<WaveSpawnerMetric> spawnersPerRound;
    
}

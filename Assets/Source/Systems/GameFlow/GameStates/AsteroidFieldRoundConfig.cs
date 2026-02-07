using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asteroid Field Round Config", menuName = "Scriptable Objects/Game Stages/Asteroid Field Round Config")]
public class AsteroidFieldRoundConfig : ScriptableObject
{
    public float timeLimit = 120;
    public List<WaveSpawnerMetric> spawnersPerRound;
    public float spawnHeight;
    public float minRadius;
    public float maxRadius;
    public GameObject[] postRoundBuffPrefabs;
}

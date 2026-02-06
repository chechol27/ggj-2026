using UnityEngine;

[CreateAssetMenu(fileName = "Asteroid Field Round Config", menuName = "Scriptable Objects/Game Stages/Asteroid Field Round Config")]
public class AsteroidFieldRoundConfig : ScriptableObject
{
    public float spawnHeight;
    public float minRadius;
    public float maxRadius;
    public GameObject[] postRoundBuffPrefabs;
}

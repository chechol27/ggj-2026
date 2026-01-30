using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public float baseHealth;
    public float baseO2;
    public float baseO2Regen;
    public float baseO2Deplete;
    public float baseDamage; 
    public float baseIntelligence;
    public float baseSpeed;
    public float sprintMultiplier;
    public float baseFireRate;
}

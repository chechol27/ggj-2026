using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HUD Database", menuName = "Scriptable Objects/HUD/HUD Database")]
public class HUDPrefabDatabase : ScriptableObject
{
    public List<HUDUI> hudPrefabs;
}

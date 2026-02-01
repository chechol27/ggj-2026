using System.Collections.Generic;
using UnityEngine;

public class BlackHoleRegistry : MonoBehaviour, IGameService
{
    private Dictionary<int, List<BlackHoleReference>> blackHoles = new Dictionary<int, List<BlackHoleReference>>();

    public void Register(int roomId, BlackHoleReference blackHole)
    {
        if (!blackHoles.ContainsKey(roomId))
        {
            blackHoles[roomId] = new List<BlackHoleReference>();
        }
        if (blackHoles[roomId].Contains(blackHole)) return;
        blackHoles[roomId].Add(blackHole);
    }

    public bool Unregister(int roomId, BlackHoleReference blackHole)
    {
        if (!blackHoles.ContainsKey(roomId)) return false;
        return blackHoles[roomId].Remove(blackHole);
    }

    public List<BlackHoleReference> GetBlackHolesByRoom(int roomId)
    {
        return blackHoles[roomId];
    }

    public void DisableBlackHoles()
    {
        foreach (List<BlackHoleReference> holes in blackHoles.Values)
        {
            foreach (BlackHoleReference blackHole in holes)
            {
                blackHole.Deactivate();
            }
        }
    }
}
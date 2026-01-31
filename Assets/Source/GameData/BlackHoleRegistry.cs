using System.Collections.Generic;
using UnityEngine;

public class BlackHoleRegistry : MonoBehaviour, IGameService
{
    private Dictionary<int, List<BlackHole>> blackHoles = new Dictionary<int, List<BlackHole>>();

    public void Register(int roomId, BlackHole blackHole)
    {
        if (!blackHoles.ContainsKey(roomId))
        {
            blackHoles[roomId] = new List<BlackHole>();
        }
        if (blackHoles[roomId].Contains(blackHole)) return;
        blackHoles[roomId].Add(blackHole);
    }

    public bool Unregister(int roomId, BlackHole blackHole)
    {
        if (!blackHoles.ContainsKey(roomId)) return false;
        return blackHoles[roomId].Remove(blackHole);
    }

    public List<BlackHole> GetBlackHolesByRoom(int roomId)
    {
        return blackHoles[roomId];
    }

    public void DisableBlackHoles()
    {

        foreach (List<BlackHole> holes in blackHoles.Values)
        {
            foreach (BlackHole blackHole in holes)
            {
                blackHole.gameObject.SetActive(false);
            }
        }
    }
}
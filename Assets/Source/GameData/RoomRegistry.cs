using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomRegistry : MonoBehaviour, IGameService
{
    public Dictionary<int, Transform> rooms = new Dictionary<int, Transform>();
    
    public List<int> SortByDistance(Vector3 position)
    {
        var deadKeys = rooms.Where(kvp => kvp.Value == null).Select(kvp => kvp.Key).ToList();
        for (int i = 0; i < deadKeys.Count; i++)
            rooms.Remove(deadKeys[i]);

        return rooms
            .OrderByDescending(kvp => (kvp.Value.position - position).sqrMagnitude)
            .Select(kvp => kvp.Key)
            .ToList();
    }


    public void Register(int roomId, Transform transform)
    {
        if (rooms.ContainsKey(roomId)) return;
        rooms[roomId] = transform;
    }
}
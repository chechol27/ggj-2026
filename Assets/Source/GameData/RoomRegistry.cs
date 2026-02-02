using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomRegistry : MonoBehaviour, IGameService
{
    public Dictionary<int, Transform> rooms = new Dictionary<int, Transform>();
    
    public List<int> SortByDistance(Vector3 position)
    {
        return rooms.Keys.OrderByDescending(x => Vector3.Distance(rooms[x].position, position)).ToList();
    }

    public void Register(int roomId, Transform transform)
    {
        if (rooms.ContainsKey(roomId)) return;
        Debug.Log($"registering room #{roomId}...");
        rooms[roomId] = transform;
    }
}
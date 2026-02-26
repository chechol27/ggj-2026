using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomRegistry : MonoBehaviour, IGameService
{
    public Dictionary<int, Vector3> rooms = new Dictionary<int, Vector3>();
    
    public List<int> SortByDistance(Vector3 position)
    {
        Debug.Log(rooms.Count);
        return rooms.Keys.OrderByDescending(x => Vector3.Distance(rooms[x], position)).ToList();
    }

    public void Register(int roomId, Transform transform)
    {
        if (rooms.ContainsKey(roomId)) return;
        Vector3 position = transform.position;
        rooms[roomId] = position;
    }

    public void CleanNulls()
    {
        rooms.Clear();
    }
}
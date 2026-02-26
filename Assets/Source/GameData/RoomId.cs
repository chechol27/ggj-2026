using UnityEngine;

public class RoomId : MonoBehaviour
{
    public int roomId;

    private void Awake()
    {
        GameServices.Get<RoomRegistry>().Register(roomId, transform);
    }

}

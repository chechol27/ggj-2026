using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class HUDUI : MonoBehaviour
{
    public void Initialize(Camera attachedCamera)
    {
        GetComponent<Canvas>().worldCamera = attachedCamera;
    }
}

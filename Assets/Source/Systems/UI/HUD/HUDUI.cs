using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class HUDUI : MonoBehaviour
{
    [SerializeField] private bool attachOnAwake;

    protected virtual void Awake()
    {
        if(attachOnAwake)
            GameServices.Get<HUD>().SetHUDUI(this);
    }

    public void Initialize(Camera attachedCamera)
    {
        GetComponent<Canvas>().worldCamera = attachedCamera;
    }
}

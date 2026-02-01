using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AnimationEventBroadcast : UnityEvent<string>
{
    
}

public class AnimationEventSender : MonoBehaviour
{
    public AnimationEventBroadcast onAnimationEvent;

    public void SendAnimationEvent(string functionName)
    {
        onAnimationEvent?.Invoke(functionName);
    }
}

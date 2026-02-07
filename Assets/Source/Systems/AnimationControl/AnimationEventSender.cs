using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class FloatAnimationEventRelay : UnityEvent<float>
{
    
}

[Serializable]
public class IntAnimationEventRelay : UnityEvent<int>
{
    
}

[Serializable]
public class StringAnimationEventRelay : UnityEvent<string>
{
    
}

[Serializable]
public class ObjectAnimationEventRelay : UnityEvent<object>
{
    
}

[Serializable]
public class AnimationEventId
{
    public string name;
    public UnityEvent relay;
}

[Serializable]
public class FloatAnimationEventId
{
    public string name;
    public FloatAnimationEventRelay relay;
}

[Serializable]
public class IntAnimationEventId
{
    public string name;
    public IntAnimationEventRelay relay;
}

[Serializable]
public class StringAnimationEventId
{
    public string name;
    public StringAnimationEventRelay relay;
}

[Serializable]
public class ObjectAnimationEventId
{
    public string name;
    public ObjectAnimationEventRelay relay;
}

public class AnimationEventSender : MonoBehaviour
{
    [SerializeField] private List<AnimationEventId> broadcastStream;
    [SerializeField] private List<FloatAnimationEventId> floatBroadcastStream;
    [SerializeField] private List<IntAnimationEventId> intBroadcastStream;
    [SerializeField] private List<StringAnimationEventId> stringBroadcastStream;
    [SerializeField] private List<ObjectAnimationEventId> objectBroadcastStream;

    public void SendAnimationEvent(string relayId)
    {
        //Debug.Log($"Broascasting animation event {relayId}");
        broadcastStream?.FirstOrDefault(x => x.name == relayId)?.relay?.Invoke();
    }
    
    public void SendFloatAnimationEvent(FloatAnimationEventData data)
    {
        //Debug.Log($"Broascasting animation event {data.relayId} || {data.param}");
        floatBroadcastStream?.FirstOrDefault(x => x.name == data.relayId)?.relay?.Invoke(data.param);
    }

    public void SendIntAnimationEvent(IntAnimationEventData data)
    {
        //Debug.Log($"Broascasting animation event {data.relayId} || {data.param}");
        floatBroadcastStream?.FirstOrDefault(x=>x.name == data.relayId)?.relay?.Invoke(data.param);
    }

    public void SendStringAnimationEvent(StringAnimationEventData data)
    {
        //Debug.Log($"Broascasting animation event {data.relayId} || {data.param}");
        stringBroadcastStream?.FirstOrDefault(x => x.name == data.relayId)?.relay?.Invoke(data.param);
    }

    public void SendObjectAnimationEvent(ObjectAnimationEventData data)
    {
        //Debug.Log($"Broascasting animation event {data.relayId} || {data.param}");
        objectBroadcastStream?.FirstOrDefault(x => x.name == data.relayId)?.relay?.Invoke(data.param);
    }
    
}

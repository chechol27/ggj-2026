using System;
using UnityEngine;

[Serializable]
public class Vector2Damper
{
    [SerializeField] private float smoothTime;
    
    [SerializeField] private Vector2 currentValue;
    private Vector2 targetValue;
    private Vector2 velocity;
    
    public void Update()
    {
        currentValue = Vector2.SmoothDamp(currentValue, targetValue, ref velocity, smoothTime);
    }

    public Vector2 CurrentValue => currentValue;

    public Vector2 TargetValue
    {
        get => targetValue;
        set => targetValue = value;
    }
}

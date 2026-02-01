using System;
using UnityEngine;

public class ReplaceFloatBuffAnimated : MonoBehaviour, IBuff
{
    public string StatName { get; set; }
    public float Duration { get; set; }
    public float MinValue { get; set; }
    public float MaxValue { get; set; }

    private float timer;

    private void Awake()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > Duration)
            Destroy(this);
    }

    public object ModifyValue(object value)
    {
        return Mathf.Lerp(MinValue, MaxValue, timer / Duration);
    }
}

using System;
using UnityEngine;

public class SimpleActivationParamater : MonoBehaviour
{
    [SerializeField] private string eventPath;
    [SerializeField] private string paramName;
    [SerializeField] private float startValue;
    [SerializeField] private float stopValue;

    private void OnEnable()
    {
        GameServices.Get<Sound>().SetParameter(eventPath, paramName, startValue);
    }

    private void OnDisable()
    {
        GameServices.Get<Sound>().SetParameter(eventPath, paramName, stopValue);

    }
}

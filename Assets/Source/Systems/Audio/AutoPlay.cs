using System;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    [SerializeField] private string eventPath;
    private void Awake()
    {
        GameServices.Get<Sound>().Play(eventPath);
    }
}

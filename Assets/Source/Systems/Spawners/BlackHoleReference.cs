using System;
using UnityEngine;
using UnityEngine.Events;

public class BlackHoleReference : MonoBehaviour
{
    [SerializeField] private int roomId;
    [SerializeField] private BlackHole blackHole;


    private void Awake()
    {
        GameServices.Get<BlackHoleRegistry>().Register(roomId, this);
        Deactivate();
    }

    private void OnDestroy()
    {
        GameServices.Get<BlackHoleRegistry>().Unregister(roomId, this);
    }

    public void Activate()
    {
        blackHole.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        blackHole.gameObject.SetActive(false);
    }

    public void RegisterRepairListener(UnityAction listener)
    {
        blackHole.GetComponent<BlackHoleHealth>().onRepaired.AddListener(listener);
    }
}

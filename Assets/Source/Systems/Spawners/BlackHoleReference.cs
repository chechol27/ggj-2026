using UnityEngine;
using UnityEngine.Events;

public class BlackHoleReference : MonoBehaviour
{
    [SerializeField] private int roomId;
    [SerializeField] private BlackHole blackHole;
    [SerializeField] private GameObject nonAggressiveBlackHole;
    
    private void Awake()
    {
        GameServices.Get<BlackHoleRegistry>().Register(roomId, this);
        Deactivate();
    }

    private void OnDestroy()
    {
        GameServices.Get<BlackHoleRegistry>().Unregister(roomId, this);
    }

    public void Activate(bool aggressive = true)
    {
        if (nonAggressiveBlackHole.activeSelf || blackHole.gameObject.activeSelf) return;
        if (aggressive)
        {
            blackHole.gameObject.SetActive(true);
        }
        else
        {
            nonAggressiveBlackHole.gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        blackHole.gameObject.SetActive(false);
        nonAggressiveBlackHole.gameObject.SetActive(false);
    }

    public void RegisterRepairListener(UnityAction listener, bool aggressive = true)
    {
        if (aggressive)
        {
            blackHole.GetComponent<BlackHoleHealth>().onRepaired.AddListener(listener);
        }
        else
        {
            nonAggressiveBlackHole.GetComponent<BlackHoleHealth>().onRepaired.AddListener(listener);
        }
    }
}

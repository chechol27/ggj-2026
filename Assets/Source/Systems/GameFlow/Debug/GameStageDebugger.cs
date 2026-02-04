using System;
using UnityEngine;

public class GameStageDebugger : MonoBehaviour
{
    [SerializeField] private bool SetOnStart;
    [SerializeField] private float autoSwitchDelay;
    [SerializeField] private GameStageType stage;

    public void SimulateStageSwitch()
    {
        GameServices.Get<GameFlow>().SwitchStage(stage);
    }

    private void Start()
    {
        if (SetOnStart)
        {
            Invoke(nameof(SimulateStageSwitch), autoSwitchDelay);
        }
    }
}

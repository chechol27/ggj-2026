using System;
using UnityEngine;

public class GameStageDebugger : MonoBehaviour
{
    [SerializeField] private bool SetOnAwake;
    [SerializeField] private GameStageType stage;

    public void SimulateStageSwitch()
    {
        GameServices.Get<GameFlow>().SwitchStage(stage);
    }

    private void Awake()
    {
        if (SetOnAwake)
        {
            SimulateStageSwitch();
        }
    }
}

using UnityEngine;

public class GameStageDebugger : MonoBehaviour
{
    [SerializeField] private GameStageType stage;

    public void SimulateStageSwitch()
    {
        GameServices.Get<GameFlow>().SwitchStage(stage);
    }
}

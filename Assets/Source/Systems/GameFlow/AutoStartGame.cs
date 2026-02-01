using UnityEngine;

[DefaultExecutionOrder(1)]
public class AutoStartGame : MonoBehaviour
{
    private void Awake()
    {
        GameServices.Get<GameFlow>().SwitchStage(GameStageType.EnemyWave);
    }
}

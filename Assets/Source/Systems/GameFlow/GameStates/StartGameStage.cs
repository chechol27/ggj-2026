using System;
using UnityEngine.SceneManagement;

public class StartGameStage : GameStage
{
    private GameFlow flow;

    private void Awake()
    {
        flow = GameServices.Get<GameFlow>();
    }

    public override void OnStateEnter()
    {
        SceneManager.LoadScene("level01", LoadSceneMode.Single);
        flow.SwitchStage(GameStageType.EnemyWave);
    }
}

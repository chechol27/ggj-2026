using System;
using UnityEngine.SceneManagement;

public class StartGameStage : GameStage
{
    private GameFlow flow;

    private Player player;
    private void Awake()
    {
        flow = GameServices.Get<GameFlow>();
        player = GameServices.Get<Player>();
    }

    public override void OnStateEnter()
    {
        player.Initialize();
        SceneManager.LoadScene("level01", LoadSceneMode.Single);
    }
}

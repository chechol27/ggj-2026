using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuStage : GameStage
{
    private GameFlow flow;

    private void Awake()
    {
        flow = GameServices.Get<GameFlow>();
    }

    public override void OnStateEnter()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}

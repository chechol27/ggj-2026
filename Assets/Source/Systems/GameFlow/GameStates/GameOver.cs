using UnityEngine.Events;

public class GameOver : GameStage
{
    
    public override void OnStateEnter()
    {
        GameServices.Get<GameFlow>().SetPause(true);
    }

    public override void OnStateExit()
    {
        GameServices.Get<GameFlow>().SetPause(false);
    }
}

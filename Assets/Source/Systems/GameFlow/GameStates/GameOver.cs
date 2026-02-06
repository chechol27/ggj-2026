public class GameOver : GameStage
{
    public override void OnStateEnter()
    {
        GameServices.Get<HUD>().SetHUDUI<GameOverHUD>();
        GameServices.Get<GameFlow>().SetPause(true);
        GameServices.Get<Score>().SendAndResetPoints();
    }

    public override void OnStateExit()
    {
        GameServices.Get<GameFlow>().SetPause(false);
    }
}

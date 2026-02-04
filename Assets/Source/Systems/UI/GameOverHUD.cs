using TMPro;
using UnityEngine;

public class GameOverHUD : HUDUI
{
    [SerializeField] private TextMeshProUGUI bodyText;
    private GameFlow flow;

    private void OnEnable()
    {
        flow = GameServices.Get<GameFlow>();
        bodyText.text = $"Tu puntaje fue de {GameServices.Get<Game>().Points}. Si te registraste al principio de la partida aparecerás en el ranking. Vuelve a intentarlo o sal al menú principal.";
    }

    public void RetryButton()
    {
        flow.SwitchStage(GameStageType.StartGame);
    }

    public void EndButton()
    {
        flow.SwitchStage(GameStageType.MainMenu);
    }

    public void OpenURL()
    {
        Application.OpenURL("https://docs.google.com/spreadsheets/d/1L5vBsGF22rwBB1LknJJE--WBD0LxTnja8pETV1AL5XU/edit?usp=sharing");
    }
}

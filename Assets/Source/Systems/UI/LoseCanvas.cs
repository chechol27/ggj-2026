using System;
using TMPro;
using UnityEngine;

public class LoseCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bodyText;
    private GameFlow flow;

    private void OnEnable()
    {
        flow = GameServices.Get<GameFlow>();
        flow.onStageChanged.AddListener(DestroyMyself);
        bodyText.text = $"Tu puntaje fue de {GameServices.Get<Game>().Points}. Si te registraste al principio de la partida aparecerás en el ranking. Vuelve a intentarlo o sal al menú principal.";
    }

    public void RetryButton()
    {
        GameServices.Get<GameFlow>().SwitchStage(GameStageType.StartGame);
    }

    public void EndButton()
    {
        GameServices.Get<GameFlow>().SwitchStage(GameStageType.MainMenu);
    }

    void DestroyMyself(GameStageType stageType)
    {
        if (stageType != GameStageType.GameOver)
        {
            Destroy(gameObject);
        }
    }
}

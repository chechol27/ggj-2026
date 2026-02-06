using System;
using UnityEngine;
using UnityEngine.Events;

public enum GameStageType
{
    None,
    MainMenu,
    StartGame,
    Interlude,
    EnemyWave,
    AsteroidField,
    GameOver
}

[Serializable]
public class GameStageEvent : UnityEvent<GameStageType>
{
    
}

public class GameFlow : MonoBehaviour, IGameService
{
    public GameStageEvent onStageChanged;

    private GameStage currentStage;

    private Game game;

    private GameStageType currentStageType;

    private void Awake()
    {
        game = GameServices.Get<Game>();
    }

    private TStage GetOrCreateStage<TStage>() where TStage : GameStage
    {
        if (TryGetComponent(out TStage stage))
        {
            return stage;
        }

        return gameObject.AddComponent<TStage>();
    }
    
    public void SwitchStage(GameStageType stageType)
    {
        GameStage lastStage = currentStage;
        currentStageType = stageType;
        switch (stageType)
        {
            case GameStageType.None:
                currentStage = null;
                return;
            case GameStageType.MainMenu:
                currentStage = GetOrCreateStage<MainMenuStage>();
                break;
            case GameStageType.StartGame:
                currentStage = GetOrCreateStage<StartGameStage>();
                break;
            case GameStageType.Interlude:
                currentStage = GetOrCreateStage<RoundInterlude>();
                break;
            case GameStageType.EnemyWave:
                currentStage = GetOrCreateStage<EnemyWave>();
                break;
            case GameStageType.AsteroidField:
                currentStage = GetOrCreateStage<AsteroidFieldRound>();
                break;
            case GameStageType.GameOver:
                currentStage = GetOrCreateStage<GameOver>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stageType), stageType, null);
        }
        lastStage?.OnStateExit();
        if (currentStage != lastStage)
        {
            currentStage?.OnStateEnter();
            onStageChanged?.Invoke(stageType);
        }
    }

    public void SetPause(bool pausedState)
    {
        game.paused = pausedState;
        Time.timeScale = pausedState ? 0 : 1;
    }

    public TStage GetCurrentStage<TStage>() where TStage : GameStage
    {
        if (currentStage is TStage castedStage)
        {
            return castedStage;
        }

        return null;
    }

    public GameStageType CurrentStageType => currentStageType;

    public GameStage CurrentStage => currentStage;
}

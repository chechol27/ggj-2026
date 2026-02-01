using System;
using UnityEngine;
using UnityEngine.Events;

public enum GameStageType
{
    None,
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
        switch (stageType)
        {
            case GameStageType.None:
                currentStage = null;
                return;
            case GameStageType.Interlude:
                currentStage?.OnStateExit();
                currentStage = GetOrCreateStage<RoundInterlude>();
                break;
            case GameStageType.EnemyWave:
                currentStage?.OnStateExit();
                currentStage = GetOrCreateStage<EnemyWave>();
                break;
            case GameStageType.AsteroidField:
                currentStage?.OnStateExit();
                currentStage = GetOrCreateStage<AsteroidFieldRound>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stageType), stageType, null);
        }
        if (currentStage != lastStage)
        {
            currentStage?.OnStateEnter();
            onStageChanged?.Invoke(stageType);
        }
    }

    public GameStage CurrentStage => currentStage;
}

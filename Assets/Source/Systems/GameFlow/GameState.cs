using System;
using UnityEngine;
using UnityEngine.Events;

public enum GameStage
{
    None,
    PreGame,
    Loop,
    PostGame
}

[Serializable]
public class GameStageEvent : UnityEvent<GameStage>
{
    
}

public class GameState : MonoBehaviour, IGameService
{
    public GameStageEvent onStageChanged;
    
    public void SwitchStage(GameStage stage)
    {
        onStageChanged?.Invoke(stage);
    }
}

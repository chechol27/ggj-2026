using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private Button startGameButton;

    void StartGame()
    {
        GameServices.Get<GameFlow>().SwitchStage(GameStageType.StartGame);
    }
    
    private void Awake()
    {
        startGameButton.onClick.AddListener(StartGame);
    }
}

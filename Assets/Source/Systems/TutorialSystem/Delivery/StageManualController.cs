using System.Collections.Generic;
using Source.Systems.TutorialSystem.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageManualControllerStandalone : MonoBehaviour
{
    [SerializeField] private StageManualDatabaseSO database;
    [SerializeField] private ManualPopupSpawner popupSpawner;

    private GameFlow gameFlow;
    private ManualGate manualGate;

    private GameStageType lastStage;
    private PlayerInput cachedPlayerInput;
    private static readonly HashSet<GameStageType> shownStages = new();

    private void Awake()
    {
        gameFlow = GameServices.Get<GameFlow>();
        manualGate = GameServices.Get<ManualGate>();
        if (gameFlow != null)
            gameFlow.onStageChanged.AddListener(OnStageChanged);
    }

    private void OnDestroy()
    {
        if (gameFlow != null)
            gameFlow.onStageChanged.RemoveListener(OnStageChanged);
    }

    private void OnStageChanged(GameStageType stage)
    {
        lastStage = stage;

        if (database == null || database.entries == null || popupSpawner == null || manualGate == null || gameFlow == null)
            return;

        if (shownStages.Contains(stage))
            return;


        var entry = database.entries.Find(e => e.stage == stage);
        if (entry == null || string.IsNullOrEmpty(entry.text))
        {
            manualGate.Close("no entry");
            return;
        }

        shownStages.Add(stage);

        manualGate.Open($"stage={stage}");
        gameFlow.SetPause(true);

        cachedPlayerInput = FindFirstObjectByType<PlayerInput>();
        if (cachedPlayerInput != null) cachedPlayerInput.enabled = false;

        var popup = popupSpawner.Show(stage, entry.text);
        popup.OnClosed += OnClosed;
    }

    private void OnClosed()
    {
        manualGate.Close("popup closed");

        if (cachedPlayerInput != null) cachedPlayerInput.enabled = true;

        gameFlow.SetPause(false);
    }

}
using System.Collections;
using UnityEngine;

public class LevelFlowStarter : MonoBehaviour
{
    [SerializeField] private float worldReadyTimeout = 3f;

    private IEnumerator Start()
    {
        var flow = GameServices.Get<GameFlow>();
        var gate = FindFirstObjectByType<ManualGate>();

        yield return null;

        // 1) Intro (muestra manual si toca)
        flow.SwitchStage(GameStageType.LevelIntro);

        // 2) Espera manual SI ABRE; si no abre, no bloquea
        if (gate != null)
        {
            float t = 0f;
            while (t < 0.5f && !gate.IsOpen) { t += Time.unscaledDeltaTime; yield return null; }
            if (gate.IsOpen)
                while (gate.IsOpen) yield return null;
        }

        // 3) Espera a que el "mundo" esté listo (rooms + player)
        yield return WaitWorldReady(worldReadyTimeout);

        // 4) Arranca la wave
        var rr = FindFirstObjectByType<RoomRegistry>();
        Debug.Log("[LevelFlowStarter] rooms count = " + (rr != null ? rr.rooms.Count : -1));
        flow.SwitchStage(GameStageType.EnemyWave);
    }

    private IEnumerator WaitWorldReady(float timeout)
    {
        float t = 0f;

        RoomRegistry roomRegistry = null;
        Player player = null;

        while (t < timeout)
        {
            if (roomRegistry == null) roomRegistry = FindFirstObjectByType<RoomRegistry>();
            if (player == null) player = FindFirstObjectByType<Player>();

            bool roomsReady = roomRegistry != null && roomRegistry.rooms != null && roomRegistry.rooms.Count > 0;

            // Ajusta esta condición si tu Player no funciona así.
            // La idea es: "ya existe el player y tiene character / posición válida"
            bool playerReady = player != null; 

            if (roomsReady && playerReady)
                yield break;

            t += Time.unscaledDeltaTime;
            yield return null;
        }

        Debug.LogWarning("[LevelFlowStarter] WorldReady timeout. Starting anyway.");
    }
}
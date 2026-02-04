using System.Linq;
using UnityEngine;

public class HUD : MonoBehaviour, IGameService
{
    private const string HUD_PREFAB_DATABASE = "HUD/HUD_Database";

    private HUDPrefabDatabase database;
    HUDUI currentUI;

    private void Awake()
    {
        database = Resources.Load<HUDPrefabDatabase>(HUD_PREFAB_DATABASE);
    }

    public void RemoveHUD()
    {
        if(currentUI != null) Destroy(currentUI.gameObject);
    }
    
    public void SetHUDUI<TUI>() where TUI : HUDUI
    {
        TUI newHUDPrefab = (TUI)database.hudPrefabs.First(hudPrefab => hudPrefab is TUI);
        RemoveHUD();
        currentUI = Instantiate(newHUDPrefab);
        currentUI.Initialize(Camera.main);
    }

    public TUI AddHUDUI<TUI>() where TUI : HUDUI
    {
        if (currentUI == null) return null;
        TUI newHUDPrefab = (TUI)database.hudPrefabs.First(hudPrefab => hudPrefab is TUI);
        TUI newHUD = Instantiate(newHUDPrefab, currentUI.transform);
        return newHUD;
    }

}

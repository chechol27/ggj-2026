using System.Linq;
using UnityEngine;


/// <summary>
/// Simple system for managing HUD screens
/// TODO: Camera.main is not a good Idea, consider creating a camera registering system
/// </summary>
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

    public void SetHUDUI<TUI>(TUI hudUI, bool persistent = false) where TUI : HUDUI
    {
        RemoveHUD();
        currentUI = hudUI;
        currentUI.Initialize(Camera.main);
    }
    
    public void SetHUDUI<TUI>(bool persistent = false) where TUI : HUDUI
    {
        TUI newHUDPrefab = (TUI)database.hudPrefabs.First(hudPrefab => hudPrefab is TUI);
        RemoveHUD();
        currentUI = Instantiate(newHUDPrefab);
        currentUI.Initialize(Camera.main);
        if(persistent) DontDestroyOnLoad(currentUI);
    }

    public TUI AddHUDUI<TUI>(bool persistent = false) where TUI : HUDUI
    {
        if (currentUI == null) return null;
        TUI newHUDPrefab = (TUI)database.hudPrefabs.First(hudPrefab => hudPrefab is TUI);
        TUI newHUD = Instantiate(newHUDPrefab, currentUI.transform);
        newHUD.Initialize(Camera.main);
        if(persistent) DontDestroyOnLoad(newHUD);
        return newHUD;
    }
}

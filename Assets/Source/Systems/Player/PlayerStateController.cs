using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;



public class PlayerStateController : MonoBehaviour, IActorComponent
{
    Player player;
    public delegate void OnPlayerStateChanged(bool distract);
    public OnPlayerStateChanged onMaskUsed;
    [SerializeField] float o2Consumption;

    [Header("States References")] 
    [SerializeField] GameObject stateCombat;
    [SerializeField] GameObject stateRepair;
    [SerializeField] GameObject stateDistract;
    
    [HideInInspector] public float maskCooldown;
    [SerializeField] private MainGameHUD HUD;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameServices.Get<Player>();
        ChangeState(stateCombat);
    }

    public void OnModeCombat(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        player.CurrentMode = PlayerMode.Combat;
        
        ChangeState(stateCombat);
    }

    public void OnModeRepair(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        player.CurrentMode = PlayerMode.Repair;
        
        ChangeState(stateRepair);
    }

    public void OnModeDistract(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        
        if (maskCooldown > 0) return;
        
        player.CurrentMode = PlayerMode.Distract;
        
        ChangeState(stateDistract);
    }

    private void FixedUpdate()
    {
        if (maskCooldown >= 0)
        {
            maskCooldown -= Time.fixedDeltaTime;
            HUD.MaskValue(12-maskCooldown);
        }
        
        if (player.CurrentMode == PlayerMode.Combat) return;
        
        player.O2 -= o2Consumption*Time.fixedDeltaTime;
        if (player.O2 <= 0)
        {
            player.CurrentMode = PlayerMode.Combat;
            ChangeState(stateCombat);
        }

    }

    void ChangeState(GameObject state)
    {
        stateCombat.SetActive(false);
        stateRepair.SetActive(false);
        stateDistract.SetActive(false);

        state.SetActive(true);
    }

    public void PrematureMaskTurnOff()
    {
        ChangeState(stateCombat);
    }


    public Actor Actor { get; set; }
}

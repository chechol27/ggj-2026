using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour, IActorComponent
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private Vector2Damper rumbleSpeed;

    public UnityEvent onShoot;
    
    private Player player;

    private Game game;
    private void Awake()
    {
        rumbleSpeed.ForceValue(Vector2.zero);
        player = GameServices.Get<Player>();
        game = GameServices.Get<Game>();
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (game.paused) return;
        if (!gameObject.activeInHierarchy) return;
        if (ctx.ReadValueAsButton() && ctx.started)
        {
            rumbleSpeed.ForceValue(new Vector2(0.5f, 1.0f));
            rumbleSpeed.TargetValue = Vector2.zero;
            
            if (weapon != null)
            {
                onShoot?.Invoke();
                if (weapon.Shoot(out DamageResponse response))
                {
                    if (response.result == DamageResult.Damaged)
                    {
                        
                    }
                }
            }
        }
    }

    private void Update()
    {
        rumbleSpeed.Update();
        Gamepad.current?.SetMotorSpeeds(rumbleSpeed.CurrentValue.x, rumbleSpeed.CurrentValue.y);
    }

    public Actor Actor { get; set; }
}

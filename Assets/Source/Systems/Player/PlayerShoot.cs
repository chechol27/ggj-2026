using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private Vector2Damper rumbleSpeed;
    
    private void Awake()
    {
        rumbleSpeed.ForceValue(Vector2.zero);
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton() && ctx.started)
        {
            rumbleSpeed.ForceValue(new Vector2(0.5f, 1.0f));
            rumbleSpeed.TargetValue = Vector2.zero;
            
            Debug.Log("Attack");
            if (weapon != null)
            {
                if (weapon.Shoot(out DamageResponse response))
                {
                    if (response.result == DamageResult.Damaged)
                    {
                        Debug.Log($"Ay! {response.receivedDamage}");
                    }
                }
            }
        }
    }

    private void Update()
    {
        rumbleSpeed.Update();
        Gamepad.current.SetMotorSpeeds(rumbleSpeed.CurrentValue.x, rumbleSpeed.CurrentValue.y);
    }
}

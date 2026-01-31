using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    
    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton())
        {
            if (weapon != null)
            {
                if (weapon.Shoot(out DamageResponse response))
                {
                    #pragma warning implement player-side processing when shoot hits;
                }
            }
        }
    }
}

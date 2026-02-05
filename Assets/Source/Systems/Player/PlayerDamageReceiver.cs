using UnityEngine;
using UnityEngine.Events;

public class PlayerDamageReceiver : MonoBehaviour , IDamageable<DamageMessage, DamageResponse>
{
    private Player player;

    public UnityEvent onDamaged;
    public UnityEvent onHealed;

    private bool canBeDamaged = true;
    
    private void Awake()
    {
        player = GameServices.Get<Player>();
    }

    void ResetIFrames()
    {
        canBeDamaged = true;
    }

    public DamageResponse TakeDamage(DamageMessage damage)
    {
        float previousHealth = player.Health;
        player.Health += damage.value;
        DamageResponse response = new();
        response.result = DamageResult.Immune;
        if (damage.value < 0 && canBeDamaged)
        {
            canBeDamaged = false;
            response.result = DamageResult.Damaged;
            player.Health += damage.value;
            Invoke(nameof(ResetIFrames), player.DamageIFrames);
            onDamaged?.Invoke();
        }
        else if (damage.value > 0)
        {
            response.result = DamageResult.Cured;
            player.Health += damage.value;

            onHealed?.Invoke();
        }
        if (player.Health <= 0)
        {
            response.result = DamageResult.Dead;
            GameServices.Get<GameFlow>().SwitchStage(GameStageType.GameOver);
        }
        return response;
    }
}

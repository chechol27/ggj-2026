using UnityEngine;

public class PlayerDamageReceiver : MonoBehaviour , IDamageable<DamageMessage, DamageResponse>
{
    private Player player;

    private void Awake()
    {
        player = GameServices.Get<Player>();
    }

    public DamageResponse TakeDamage(DamageMessage damage)
    {
        float previousHealth = player.Health;
        player.Health += damage.value;
        DamageResponse response = new();
        if (player.Health <= 0)
        {
            response.result = DamageResult.Dead;
            GameServices.Get<GameFlow>().SwitchStage(GameStageType.GameOver);
        }
        if (Mathf.Approximately(previousHealth, player.Health)) response.result = DamageResult.Immune;
        if (player.Health > previousHealth) response.result = DamageResult.Cured;
        return response;
    }
}

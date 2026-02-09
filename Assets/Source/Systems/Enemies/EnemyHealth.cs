using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable<DamageMessage, DamageResponse>, IActorComponent
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;

    public UnityEvent onDeath;
    public UnityEvent onDamaged;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void DisableParent()
    {
        transform.parent.gameObject.SetActive(false);
    }
    
    public void PerformDeath()
    {
        Invoke(nameof(DisableParent), 0.5f);
    }

    public DamageResponse TakeDamage(DamageMessage damage)
    {
        DamageResponse response = new DamageResponse();
        response.receivedDamage = damage.value;
        currentHealth -= damage.value;

        if (currentHealth <= 0 && damage.value > 0)
        {
            response.result = DamageResult.Dead;
            onDeath?.Invoke();
        }
        else if (damage.value > 0) // ← IMPORTANTE
        {
            response.result = DamageResult.Damaged;
            onDamaged?.Invoke();
        }
        return response;
    }

    public Actor Actor { get; set; }
}

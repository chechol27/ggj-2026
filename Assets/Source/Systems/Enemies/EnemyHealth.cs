using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable<DamageMessage, DamageResponse>, IActorComponent
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;

    public UnityEvent onDeath;
    
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
        Invoke(nameof(DisableParent), 1.5f);
    }
    
    public DamageResponse TakeDamage(DamageMessage damage)
    {
        DamageResponse response = new DamageResponse();
        response.receivedDamage = damage.value;
        currentHealth -= damage.value;
        
        response.result = currentHealth <= 0 ? DamageResult.Dead : DamageResult.Damaged;
        
        if (currentHealth <= 0)
        {
            onDeath?.Invoke();
        }
        return response;
    }

    public Actor Actor { get; set; }
}

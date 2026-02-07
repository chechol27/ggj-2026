using UnityEngine;
using UnityEngine.Events;

public class BlackHoleHealth : MonoBehaviour, IDamageable<float, bool>
{
    [SerializeField] private float maxHalth;
    
    [SerializeField] private float currentHealth;

    public UnityEvent onDeath;
    
    public void Awake()
    {
        currentHealth = maxHalth;
    }

    public bool TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameServices.Get<GameFlow>().GetCurrentStage<RoundStage>()?.HandleSpawnerRepair();
            onDeath?.Invoke();
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnerHealth : MonoBehaviour, IDamageable<float, bool>
{
    [SerializeField] private float maxHalth;

    public UnityEvent onRepaired;

    private float currentHealth;

    public void Awake()
    {
        currentHealth = maxHalth;
    }

    public bool TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            onRepaired?.Invoke();
            return true;
        }

        return false;
    }
}

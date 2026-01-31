using UnityEngine;

public class EnemySpawnerHealth : MonoBehaviour, IDamageable<float, bool>
{
    [SerializeField] private float maxHalth;


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
            return true;
        }

        return false;
    }
}

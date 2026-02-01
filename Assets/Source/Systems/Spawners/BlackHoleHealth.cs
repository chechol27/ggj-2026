using UnityEngine;
using UnityEngine.Events;

public class BlackHoleHealth : MonoBehaviour, IDamageable<float, bool>
{
    [SerializeField] private float maxHalth;

    public UnityEvent onRepaired;

    [SerializeField] private float currentHealth;

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

    private void OnDisable()
    {
        onRepaired = null;
    }
}

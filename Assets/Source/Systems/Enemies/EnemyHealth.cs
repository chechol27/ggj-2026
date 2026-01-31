using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable<DamageMessage, DamageResponse>
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public DamageResponse TakeDamage(DamageMessage damage)
    {
        DamageResponse response = new DamageResponse();
        response.receivedDamage = damage.value;
        currentHealth -= damage.value;
        
        response.result = currentHealth <= 0 ? DamageResult.Dead : DamageResult.Damaged;
        
        Debug.Log("Damaged");
        if (currentHealth <= 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
        return response;
    }
}

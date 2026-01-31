using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable<DamageMessage, DamageResponse>
{
    [SerializeField] float maxHealth = 100;
    float currentHealth;

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
        
        return response;
    }
}

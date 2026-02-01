using System;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAttacks : MonoBehaviour
{
    [SerializeField] float attackRange;
    [SerializeField] float attackRangeOffset;
    MeleeEnemyMovement movement;

    private void OnEnable()
    {
        movement = GetComponent<MeleeEnemyMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() == null) return;
        movement.CancelMove();
        
        //DELAY TEMPORAL SIMULANDO LA ANIMACIÓN
        Invoke(nameof(TempInvokeDamage), 0.5f);
    }

    //FUNCIÓN TEMPORAL SIMULANDO LA ANIMACIÓN
    void TempInvokeDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward*attackRangeOffset, attackRange);
        damagePlayer(colliders);
    }

    void damagePlayer(Collider[] damagedItems)
    {
        foreach (Collider item in damagedItems)
        {
            if(item.gameObject.TryGetComponent<PlayerDamageReceiver>(out PlayerDamageReceiver damageable))
            {
                DamageMessage payload =  new DamageMessage
                {
                    factionId =  0,
                    hitPoint = item.transform.position,
                    value = -20
                };
                damageable.TakeDamage(payload);
            }
        }
        movement.Move();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + transform.forward*attackRangeOffset, attackRange);
    }
}

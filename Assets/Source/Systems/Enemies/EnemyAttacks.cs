using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] float attackRange;
    [SerializeField] float attackRangeOffset;
    EnemyMovement movement;

    private void OnEnable()
    {
        movement = GetComponent<EnemyMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() == null) return;
        movement.CancelMove();
        
        //DELAY TEMPORAL SIMULANDO LA ANIMACIÓN
        Invoke("TempInvokeDamage", 0.5f);
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
            if (item.GetComponent<PlayerMovement>() != null)
            {
                //Hacer daño
                Debug.Log("Daño hecho");
                break;
            }
        }
        movement.Move();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + transform.forward*attackRangeOffset, attackRange);
    }
}

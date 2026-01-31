using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] float attackRange;
    [SerializeField] float attackRangeOffset;
    EnemyMovement movement;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() == null) return;
        movement.CancelMove();
        Invoke("TempInvokeDamage", 1f);
    }

    void TempInvokeDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
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

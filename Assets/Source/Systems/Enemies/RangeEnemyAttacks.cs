using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyAttacks : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float attackDelay;
    EnemyMovement movement;
    private Player player;
    
    void OnEnable()
    {
        movement = GetComponent<EnemyMovement>();
        InvokeRepeating("CheckRange", 0.5f, 1.5f);
        player = GameServices.Get<Player>();
    }

    void CheckRange()
    {
        if (Vector3.Distance(player.CharacterPosition, transform.position) <= range)
        {
            
        }
    }

    void Attack(Vector3 target)
    {
        
    }
} 

using System;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyAttacks : MonoBehaviour, IActorComponent
{
    [SerializeField] float attackDelay;
    [SerializeField] float lowRange;
    [SerializeField] float topRange;
    private float attackCooldown;
    private bool canAttack = true;

    private Vector3 playerTarget;

    public delegate void OnProjectileLaunched(Vector3 origin, Vector3 target, float delay);
    public OnProjectileLaunched onProjectileLaunched;
    
    RangedEnemyMovement movement;
    private Player player;
    
    void OnEnable()
    {
        movement = GetComponent<RangedEnemyMovement>();
        player = GameServices.Get<Player>();
    }

    private void FixedUpdate()
    {
        if (!canAttack)
        {
            attackCooldown -= Time.deltaTime;
        }

        if (attackCooldown <= 0)
        {
            canAttack = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canAttack)
        {
            GetComponent<RangedEnemyMovement>().CancelMove();
            playerTarget = player.CharacterPosition;
            canAttack = false;
            onProjectileLaunched?.Invoke(transform.position, playerTarget, attackDelay);
            Invoke(nameof(Attack), attackDelay);
        }
    }

    void Attack()
    {
        Collider[] collision = Physics.OverlapSphere(playerTarget, 1, 1 << LayerMask.NameToLayer("Player"));
        foreach (Collider item in collision)
        {
            if(item.gameObject.TryGetComponent<PlayerDamageReceiver>(out PlayerDamageReceiver damageable))
            {
                DamageMessage payload =  new DamageMessage
                {
                    factionId =  0,
                    hitPoint = playerTarget,
                    value = -20
                };
                damageable.TakeDamage(payload);
            }
        }
        GetComponent<RangedEnemyMovement>().Move();
    }

    public Actor Actor { get; set; }
} 

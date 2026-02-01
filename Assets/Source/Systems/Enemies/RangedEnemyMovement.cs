using System;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyMovement : MonoBehaviour
{
    private Player player;
    NavMeshAgent agent;
    [SerializeField] private float runawaySpeed = 5f;
    [SerializeField] private float ApproachSpeed = 1.5f;
    private float moveDelay = 1.5f;
    
    void OnEnable()
    {
        player = GameServices.Get<Player>();
        agent = GetComponent<NavMeshAgent>();
        if(agent.isOnNavMesh)
            agent.updateRotation = true;
        Move();
    }

    void SetNavMeshTarget()
    {
        float distance = Vector3.Distance(transform.position, player.CharacterPosition);
        if (distance < agent.stoppingDistance)
        {
            agent.speed = runawaySpeed;
            Vector3 away = (transform.position-player.CharacterPosition).normalized;
            Vector3 targetPosition = transform.position + away * (agent.stoppingDistance*2);
            agent.SetDestination(targetPosition);
        }
        else
        {
            agent.speed = ApproachSpeed;
            agent.SetDestination(player.CharacterPosition);
        }
    }

    private void FixedUpdate()
    {
        moveDelay -= Time.fixedDeltaTime;
        if (moveDelay <= 0f)
        {
            SetNavMeshTarget();
            moveDelay = 1.5f;
        }
    }

    public void Move()
    {
        if(agent.isOnNavMesh)
            agent.isStopped = false;
    }

    public void CancelMove()
    {
        
        if(agent.isOnNavMesh)
            agent.isStopped = true;
    }

    private void OnDisable()
    {
        CancelMove();
    }
}

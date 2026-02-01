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
    
    private Vector3 maskTarget;
    private float resetTarget;
    bool MaskUsed = false;
    
    void OnEnable()
    {
        player = GameServices.Get<Player>();
        agent = GetComponent<NavMeshAgent>();
        if(agent.isOnNavMesh)
            agent.updateRotation = true;
        Move();
        maskTarget = player.CharacterPosition;
    }

    void SetNavMeshTarget()
    {
        if (!MaskUsed)
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
        else
        {
            agent.speed = ApproachSpeed;
            agent.SetDestination(maskTarget); 
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
        
        if (!MaskUsed) return;
        resetTarget -= Time.fixedDeltaTime;
        if (resetTarget >= 0) return;
        ChangeTarget(player.CharacterPosition, 10, false);
    }

    public void ChangeTarget(Vector3 newTarget, float time, bool used)
    {
        maskTarget = newTarget;
        resetTarget = time;
        MaskUsed = used;
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

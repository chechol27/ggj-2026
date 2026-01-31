using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Player player;
    NavMeshAgent agent;
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
        agent.SetDestination(player.CharacterPosition);
    }

    public void Move()
    {
        if(agent.isOnNavMesh)
            agent.isStopped = false;
        InvokeRepeating("SetNavMeshTarget", 0.5f, 1.5f);
    }

    public void CancelMove()
    {
        CancelInvoke("SetNavMeshTarget");
        if(agent.isOnNavMesh)
            agent.isStopped = true;
    }

    private void OnDisable()
    {
        CancelMove();
    }
}

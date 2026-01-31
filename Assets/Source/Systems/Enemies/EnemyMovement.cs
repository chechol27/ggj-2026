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
        agent.updateRotation = true;
        agent.isStopped = false;
        Move();
    }

    void SetNavMeshTarget()
    {
        agent.SetDestination(player.CharacterPosition);
    }

    public void Move()
    {
        agent.isStopped = false;
        InvokeRepeating("SetNavMeshTarget", 0.5f, 1.5f);
    }

    public void CancelMove()
    {
        agent.isStopped = true;
        CancelInvoke("SetNavMeshTarget");
    }

    private void OnDisable()
    {
        CancelMove();
    }
}

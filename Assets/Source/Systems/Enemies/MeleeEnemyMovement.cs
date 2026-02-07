using System;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyMovement : MonoBehaviour, IActorComponent
{
    private Player player;
    NavMeshAgent agent;
    private Vector3 maskTarget;
    private float resetTarget;
    bool MaskUsed = false;
    private MeleeEnemyAttacks attackScript;
    

    void OnEnable()
    {
        player = GameServices.Get<Player>();
        agent = GetComponent<NavMeshAgent>();
        if(agent.isOnNavMesh)
            agent.updateRotation = true;

        DistractMaskController tempMask = FindAnyObjectByType<DistractMaskController>();

        if (player.CurrentMode == PlayerMode.Distract || tempMask != null)
        {
            MaskUsed = true;
            if(tempMask != null)
            {
                maskTarget = tempMask.transform.position;
            }
        }
        
        Move();
        attackScript = GetComponent<MeleeEnemyAttacks>();
    }

    void SetNavMeshTarget()
    {
        if(MaskUsed)
            agent.SetDestination(maskTarget);
        else
            agent.SetDestination(player.CharacterPosition);
    }

    public void Move()
    {
        if(agent.isOnNavMesh)
            agent.isStopped = false;
        InvokeRepeating(nameof(SetNavMeshTarget), 0.5f, 1.5f);
    }

    private void FixedUpdate()
    {
        if (!MaskUsed) return;
        resetTarget -= Time.fixedDeltaTime;
        if (resetTarget >= 0) return;
        ChangeTarget(Vector3.zero, 10, false);

    }

    public void ChangeTarget(Vector3 newTarget, float time, bool used)
    {
        maskTarget = newTarget;
        resetTarget = time;
        MaskUsed = used;
        attackScript.enabled = !used;
    }

    public void CancelMove()
    {
        CancelInvoke(nameof(SetNavMeshTarget));
        if(agent.isOnNavMesh)
            agent.isStopped = true;
    }

    private void OnDisable()
    {
        CancelMove();
    }

    public Actor Actor { get; set; }
}

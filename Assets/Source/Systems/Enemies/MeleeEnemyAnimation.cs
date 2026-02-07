using UnityEngine;
using UnityEngine.Events;

public class MeleeEnemyAnimation : MonoBehaviour, IActorComponent
{
    [SerializeField] private Animator animator;
    
    public void SetAttackStage()
    {
        animator.SetTrigger("Attack");
    }

    public void SetDeathState(bool deathState)
    {
        animator.SetBool("Death", deathState);
    }

    private void Awake()
    {
        SetDeathState(false);
    }

    public Actor Actor { get; set; }
}

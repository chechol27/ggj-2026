using UnityEngine;

public class MeleeEnemyAnimation : MonoBehaviour
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
}

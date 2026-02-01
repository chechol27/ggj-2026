using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Player player;

    private void Awake()
    {
        player = GameServices.Get<Player>();
    }

    public void ShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    public void SetRepairState(bool repairState)
    {
        Debug.Log($"{(repairState ? "Starting" : "Ending")} repair animation");
        animator.SetBool("Repair", repairState);
    }

    void SetMotionVector()
    {
        animator.SetFloat("Speed", player.CharacterSpeed);
    }
    
    private void Update()
    {
        SetMotionVector();
    }
}

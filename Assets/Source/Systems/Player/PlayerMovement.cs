using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    protected Player player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector2Damper inputTransformer;
    private bool wantsToSprint;
    private Vector3 motionVector;
    
    private void Awake()
    {
        player = GameServices.Get<Player>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        inputTransformer.TargetValue = ctx.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        wantsToSprint = ctx.ReadValueAsButton();
    }

    private bool CanSprint()
    {
        if (!wantsToSprint)
        {
            return wantsToSprint;
        }

        return player.O2 > player.O2Deplete;
    }

    private void ComputeMotionVector()
    {
        Transform mainCamera = Camera.main.transform;
        Vector3 characterUp = Vector3.up;
        Vector2 input = inputTransformer.CurrentValue;
        float forwardVectorWeight = Mathf.Abs(Vector3.Dot(mainCamera.forward, characterUp));
        Vector3 rotationAgnosticForward = Vector3.Lerp(mainCamera.forward, mainCamera.up, forwardVectorWeight);
        Vector3 projectedForward = Vector3.ProjectOnPlane(rotationAgnosticForward, characterUp).normalized;
        motionVector = projectedForward * input.y + mainCamera.right * input.x;
    }

    private void Move()
    {
        if(!player.CanMove) return;
        motionVector *= player.Speed;// * (CanSprint() ? player.SprintMultiplier : 1.0f);
        if (CanSprint())
        {
            motionVector *= player.SprintMultiplier;
            player.O2 -= player.O2Deplete * Time.fixedDeltaTime;
        }
        else
        {
            player.O2 += player.O2Regen * Time.fixedDeltaTime;
        }
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, motionVector, Time.fixedDeltaTime * 15.0f);
    }
    
    private void FixedUpdate()
    {
        inputTransformer.Update();
        ComputeMotionVector();
        Move();
        player.CharacterPosition = transform.position;
    }
}

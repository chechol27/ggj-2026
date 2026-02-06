using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour, IActorComponent<PlayerCharacter>
{
    [SerializeField] private Rigidbody target;

    private Vector3 aimVector;
    private Vector3 persistentAimVector;

    private Game game;

    private void Awake()
    {
        game = GameServices.Get<Game>();
    }

    //Joystick
    public void OnLook(InputAction.CallbackContext ctx)
    {
        if (game.paused) return;
        Vector2 aim = ctx.ReadValue<Vector2>();
        if (aim.magnitude > 0)
        {
            Transform mainCamera = Camera.main.transform;
            Vector3 characterUp = target.transform.up;
            float forwardVectorWeight = Mathf.Abs(Vector3.Dot(mainCamera.forward, characterUp));
            Vector3 rotationAgnosticForward = Vector3.Lerp(mainCamera.forward, mainCamera.up, forwardVectorWeight);
            Vector3 projectedForward = Vector3.ProjectOnPlane(rotationAgnosticForward, characterUp).normalized;
            Vector3 characterCentered = projectedForward * aim.y + mainCamera.right * aim.x;
            persistentAimVector += characterCentered;
            aimVector = characterCentered.normalized;
        }
    }

    //Mouse
    public void OnAim(InputAction.CallbackContext ctx)
    {
        if (game.paused) return;
        Vector2 pointer = ctx.ReadValue<Vector2>();
        float characterDepth = Vector3.Distance(Camera.main.transform.position, target.position);
        Vector3 pointerWS = new (pointer.x, pointer.y, characterDepth);
        pointerWS = Camera.main.ScreenToWorldPoint(pointerWS);
        Vector3 projectedRelative = Vector3.ProjectOnPlane(pointerWS - target.position, Vector3.up).normalized;
        Debug.DrawLine(target.position, target.position + projectedRelative);
        Debug.DrawLine(Camera.main.transform.position, pointerWS, Color.aquamarine);
        aimVector = projectedRelative;
    }

    private void FixedUpdate()
    {
        if(aimVector.magnitude > 0)
            target.rotation = Quaternion.LookRotation(aimVector, Vector3.up);
    }

    public Actor Actor { get; set; }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour , IGameService
{
    [SerializeField] private Transform target;

    private Vector3 aimVector;
    private Vector3 persistentAimVector;
    
    //Joystick
    public void OnLook(InputAction.CallbackContext ctx)
    {
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
        Vector2 pointer = ctx.ReadValue<Vector2>();
        float characterDepth = Vector3.Distance(Camera.main.transform.position, target.position);
        Vector3 pointerWS = new Vector3(pointer.x, pointer.y, characterDepth);
        pointerWS = Camera.main.ScreenToWorldPoint(pointerWS);
        Vector3 projectedRelative = Vector3.ProjectOnPlane(pointerWS - target.position, target.up).normalized;
        Debug.DrawLine(target.position, target.position + projectedRelative);
        Debug.DrawLine(Camera.main.transform.position, pointerWS, Color.aquamarine);
        aimVector = projectedRelative;
    }

    private void Update()
    {
        target.forward = aimVector;
        Debug.DrawLine(target.position, target.position + persistentAimVector.normalized);
    }
}

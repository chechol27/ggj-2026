using Source.Systems.ModelViewer.InputControl;
using UnityEngine;
using UnityEngine.InputSystem;

public class ViewerLookRotator : MonoBehaviour
{
    [Header("Input Actions")] [SerializeField]
    private InputActionAsset actions;

    [SerializeField] private string actionMapName = "Player";
    [SerializeField] private string lookActionName = "Look";

    [Header("Target")] [SerializeField] private Transform targetPivot;

    [Header("Rotation Area")] [SerializeField]
    private PointerInsideRectGate rotationGate;

    [Header("Sensitivity")] [SerializeField]
    private float mouseSensitivity = 0.15f;

    [SerializeField] private float stickSensitivity = 120f;

    [Header("Mouse")] [SerializeField] private int mouseButton = 0; // 0 = Left
    [SerializeField] private bool requireMouseHold = true;

    private InputAction lookAction;
    private float yaw;

    private void Awake()
    {
        var map = actions.FindActionMap(actionMapName, true);
        lookAction = map.FindAction(lookActionName, true);
        CacheFromTarget();
    }

    private void OnEnable() => lookAction.Enable();
    private void OnDisable() => lookAction.Disable();

    private void Update()
    {
        if (targetPivot == null) return;

        Vector2 look = lookAction.ReadValue<Vector2>();
        if (look.sqrMagnitude < 0.0001f) return;

        bool isMouse = lookAction.activeControl?.device is Mouse;
        bool isGamepad = lookAction.activeControl?.device is Gamepad;

        if (isMouse)
        {
            if (rotationGate != null && !rotationGate.IsPointerInside)
                return;

            if (requireMouseHold && !IsMousePressed())
                return;

            yaw += look.x * mouseSensitivity;
        }
        else if (isGamepad)
        {
            yaw += look.x * stickSensitivity * Time.deltaTime;
        }
        else
        {
            yaw += look.x * stickSensitivity * Time.deltaTime;
        }

        Apply();
    }

    private bool IsMousePressed()
    {
        if (Mouse.current == null) return false;

        return mouseButton switch
        {
            0 => Mouse.current.leftButton.isPressed,
            1 => Mouse.current.rightButton.isPressed,
            2 => Mouse.current.middleButton.isPressed,
            _ => false
        };
    }

    private void Apply()
    {
        targetPivot.localRotation = Quaternion.Euler(0f, yaw, 0f);
    }

    public void SetTarget(Transform t)
    {
        targetPivot = t;
        CacheFromTarget();
    }

    public void ResetRotation()
    {
        yaw = 0f;
        Apply();
    }

    private void CacheFromTarget()
    {
        if (targetPivot == null) return;
        float y = targetPivot.localEulerAngles.y;
        if (y > 180f) y -= 360f;
        yaw = y;
    }
}
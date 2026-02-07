using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Systems.ModelViewer.InputControl
{
    public class ViewerLookRotator : MonoBehaviour
    {
        [Header("Input Actions")] [SerializeField]
        private InputActionAsset actions;

        [SerializeField] private string actionMapName = "Player";
        [SerializeField] private string lookActionName = "Look";

        [Header("Target")] [SerializeField] private Transform targetPivot; 

        [Header("Tuning")] [SerializeField] private float mouseSensitivity = 0.15f; 
        [SerializeField] private float stickSensitivity = 120f;
        [SerializeField] private bool invertX = false;
        [SerializeField] private bool invertY = false;

        [Header("Pitch Clamp")] [SerializeField]
        private bool clampPitch = true;

        [SerializeField] private float minPitch = -35f;
        [SerializeField] private float maxPitch = 35f;

        [Header("Mouse Behavior")] [SerializeField]
        private int mouseButton = 0;

        [SerializeField] private bool requireMouseHoldToRotate = true;

        private InputAction _look;
        private float yaw;
        private float pitch;

        private void Awake()
        {
            if (targetPivot == null)
                targetPivot = transform;

            var map = actions.FindActionMap(actionMapName, throwIfNotFound: true);
            _look = map.FindAction(lookActionName, throwIfNotFound: true);

            CacheAnglesFromTarget();
        }

        private void OnEnable()
        {
            _look.Enable();
        }

        private void OnDisable()
        {
            _look.Disable();
        }

        private void Update()
        {
            if (targetPivot == null) return;

            Vector2 look = _look.ReadValue<Vector2>();
            if (look.sqrMagnitude < 0.0001f) return;

            bool usingMouse = Mouse.current != null && _look.activeControl?.device is Mouse;
            bool usingGamepad = _look.activeControl?.device is Gamepad;

            if (usingMouse && requireMouseHoldToRotate)
            {
                if (Mouse.current == null) return;

                bool pressed =
                    mouseButton == 0 ? Mouse.current.leftButton.isPressed :
                    mouseButton == 1 ? Mouse.current.rightButton.isPressed :
                    Mouse.current.middleButton.isPressed;

                if (!pressed) return;
            }

            float signX = invertX ? -1f : 1f;
            float signY = invertY ? -1f : 1f;

            if (usingMouse)
            {
                yaw += look.x * mouseSensitivity * signX;
                pitch -= look.y * mouseSensitivity * signY;
            }
            else if (usingGamepad)
            {
                yaw += look.x * stickSensitivity * Time.deltaTime * signX;
                pitch -= look.y * stickSensitivity * Time.deltaTime * signY;
            }
            else
            {
                yaw += look.x * stickSensitivity * Time.deltaTime * signX;
                pitch -= look.y * stickSensitivity * Time.deltaTime * signY;
            }

            if (clampPitch)
                pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            Apply();
        }

        public void SetTarget(Transform t)
        {
            targetPivot = t;
            CacheAnglesFromTarget();
        }

        public void ResetRotation()
        {
            yaw = 0f;
            pitch = 0f;
            Apply();
        }

        private void CacheAnglesFromTarget()
        {
            var e = targetPivot.localEulerAngles;
            yaw = NormalizeAngle(e.y);
            pitch = NormalizeAngle(e.x);
        }

        private void Apply()
        {
            targetPivot.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        private float NormalizeAngle(float a)
        {
            while (a > 180f) a -= 360f;
            while (a < -180f) a += 360f;
            return a;
        }
    }
}
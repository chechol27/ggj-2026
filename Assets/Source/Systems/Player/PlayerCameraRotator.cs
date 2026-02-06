using System.Collections;
using LitMotion;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraRotator : MonoBehaviour
{
    [SerializeField] private Transform cameraRotator;
    [SerializeField] private float rotationDuration = 0.2f;

    private const float ROTATION_ANGLE = 90.0f;

    [SerializeField] private bool isRotating;

    // IEnumerator PerformRotation(float value)
    // {
    //     isRotating = true;
    //     float deltaRot = value * (Time.deltaTime / rotationDuration);
    //     for (float f = 0; f < rotationDuration; f+= Time.deltaTime)
    //     {
    //         cameraRotator.Rotate(Vector3.up * deltaRot);
    //         yield return null;
    //     }
    //
    //     isRotating = false;
    // }

    void PerformRotation(float value)
    {
        LMotion.Create(cameraRotator.localEulerAngles.y, cameraRotator.localEulerAngles.y + value, rotationDuration)
            .WithEase(Ease.InOutCubic)
            .WithOnComplete(() => isRotating = false)
            .WithScheduler(MotionScheduler.FixedUpdate)
            .Bind(val => cameraRotator.localEulerAngles = Vector3.up * val);
    }
    
    public void RotateRight(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton() && !isRotating)
        {
            isRotating = true;
            PerformRotation(-ROTATION_ANGLE);
        }
    }

    public void RotateLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton() && !isRotating)
        {
            isRotating = true;
            PerformRotation(ROTATION_ANGLE);
        }
    }
    
    
}

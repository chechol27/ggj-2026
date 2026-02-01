using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRepairController : MonoBehaviour
{
    private bool _active;
    [SerializeField] private float repairRange;
    [SerializeField] private BlackHoleHealth activeSpawner;
    private Player player;

    private void Start()
    {
        player = GameServices.Get<Player>();
        activeSpawner = null;
        _active = false;
    }

    public void OnRepairing(InputAction.CallbackContext ctx)
    {
        if(!gameObject.activeInHierarchy) return;
        if (ctx.performed)
        {
            DetectBlackHole();
        }
        if (ctx.canceled)
        {
            _active = false;
            activeSpawner = null;
            player.CanMove = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_active) return;
        RepairBlackHole();
    }

    void RepairBlackHole()
    {
        if (activeSpawner == null)
        { 
            _active = false;
            player.CanMove = true;
        }
        else
        {
            activeSpawner.TakeDamage(player.Intelligence * Time.fixedDeltaTime);
        }
    }

    void DetectBlackHole()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, repairRange);
        if (objects.Length <= 0)
        {
            Debug.Log("Arreglo vacío");
            return;
        }
        foreach (Collider obj in objects)
        {
            if (obj.gameObject.TryGetComponent<BlackHoleHealth>(out BlackHoleHealth active))
            {
                _active = true;
                activeSpawner = active;
                player.CanMove = false;
                break;
            }
        }
    }

    private void OnDisable()
    {
        player.CanMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, repairRange);
    }
}

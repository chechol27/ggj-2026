using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerRepairController : MonoBehaviour
{
    private bool _active;
    [SerializeField] private float repairRange;
    [SerializeField] private BlackHoleHealth activeSpawner;
    private Player player;

    public UnityEvent onStartRepair;
    public UnityEvent onEndRepair;

    private Game game;
    void SetActivationState(bool state)
    {
        _active = state;
        if (state)
        {
            onStartRepair?.Invoke();
        }
        else
        {
            onEndRepair?.Invoke();
        }
    }

    private void Awake()
    {
        game = GameServices.Get<Game>();
    }

    private void Start()
    {
        player = GameServices.Get<Player>();
        activeSpawner = null;
        SetActivationState(false);
    }

    public void OnRepairing(InputAction.CallbackContext ctx)
    {
        if (game.paused) return;
        if(!gameObject.activeInHierarchy) return;
        if (ctx.performed)
        {
            DetectBlackHole();
        }
        if (ctx.canceled)
        {
            SetActivationState(false);
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
            SetActivationState(false);
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
                SetActivationState(true);
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

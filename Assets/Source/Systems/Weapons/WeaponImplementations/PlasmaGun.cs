using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PlasmaGun : Weapon
{
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float impulseForce;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private GameObject VFXPrefab;
    [SerializeField] private Transform graphicalMuzzle;
    [SerializeField] private float reloadRate = 6.0f;
    [SerializeField] private float maxEnergy = 10;
    [SerializeField] private float energyPerShot = 1;

    [SerializeField]private float currentEnergy;
    public UnityEvent OnShoot;
    private Player player;

    private bool isReloading;
    
    private void Awake()
    {
        player = GameServices.Get<Player>();
        currentEnergy = maxEnergy;
    }

    private void Update()
    {
        if (isReloading)
        {
            currentEnergy += reloadRate * Time.deltaTime;
            if (currentEnergy >= maxEnergy)
            {
                currentEnergy = maxEnergy;
                isReloading = false;
            }
        }
    }

    public override bool Shoot(out DamageResponse response, Transform logicalMuzzle = null)
    {
        response = default;
        if (isReloading)
        {
            return false;
        }
        currentEnergy -= energyPerShot;
        if (currentEnergy <= 0)
        {
            isReloading = true;
        }
        float zLength = 100;
        OnShoot?.Invoke();
        logicalMuzzle = logicalMuzzle == null ? graphicalMuzzle : logicalMuzzle;
        Ray r = new (logicalMuzzle.position, logicalMuzzle.forward);
        Debug.DrawRay(logicalMuzzle.position, logicalMuzzle.forward * 20, Color.red, 5);
        if(Physics.SphereCast(r, 0.05f, out RaycastHit hit,Mathf.Infinity, detectionMask))
        {
            if (hit.collider.TryGetComponent(out IDamageable<DamageMessage, DamageResponse> damageable))
            {
                DamageMessage message = new()
                {
                    value = player.Damage,
                    hitPoint = hit.point
                };
                response = damageable.TakeDamage(message);
                zLength = Vector3.Distance(hit.collider.gameObject.transform.position, graphicalMuzzle.position);
            }
        }
        GameServices.Get<Pool>().Spawn(
            VFXPrefab, 
            out GameObject particleSystem, 
            TransformFrame.TRS(graphicalMuzzle.position, graphicalMuzzle.rotation, new Vector3(1,1,zLength * 2.0f)));
        //particleSystem.transform.parent = muzzle;
        impulseSource.GenerateImpulse(impulseForce);
        return true;
    }
}

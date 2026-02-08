using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PlasmaGun : Weapon
{
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float impulseForce;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private GameObject VFXPrefab;
    [SerializeField] private Transform muzzle;

    public UnityEvent OnShoot;

    private Player player;

    private void Awake()
    {
        player = GameServices.Get<Player>();
    }

    public override bool Shoot(out DamageResponse response)
    {
        float zLength = 100;
        bool ret = false;
        response = default;
        OnShoot?.Invoke();
        Ray r = new (muzzle.position, muzzle.forward);
        Debug.DrawRay(r.origin, r.direction * 20, Color.red, 5);
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
                zLength = Vector3.Distance(hit.collider.gameObject.transform.position, muzzle.position);
                ret = true;
            }
        }
        GameServices.Get<Pool>().Spawn(
            VFXPrefab, 
            out GameObject particleSystem, 
            TransformFrame.TRS(muzzle.position, muzzle.rotation, new Vector3(1,1,zLength * 2.0f)));
        particleSystem.transform.parent = muzzle;
        impulseSource.GenerateImpulse(impulseForce);
        return ret;
    }
}

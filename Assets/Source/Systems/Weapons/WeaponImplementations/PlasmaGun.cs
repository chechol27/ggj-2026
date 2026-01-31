using Unity.Cinemachine;
using UnityEngine;

public class PlasmaGun : Weapon
{
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float impulseForce;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private GameObject VFXPrefab;
    [SerializeField] private Transform muzzle;

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

        Ray r = new Ray(muzzle.position, muzzle.forward);
        Debug.DrawRay(r.origin, r.direction * 20, Color.red, 5);
        if (Physics.Raycast(r, out RaycastHit hit, Mathf.Infinity, detectionMask))
        {
            if (hit.collider.TryGetComponent(out IDamageable<DamageMessage, DamageResponse> damageable))
            {
                DamageMessage message = new DamageMessage();
                message.value = player.Damage;
                message.hitPoint = hit.point;
                response = damageable.TakeDamage(message);
                zLength = Vector3.Distance(hit.point, muzzle.position);
                #pragma warning TODO Handle damage after successful shot
                ret = true;
            }
        }
        GameServices.Get<Pool>().Spawn(
            VFXPrefab, 
            out GameObject particleSystem, 
            TransformFrame.TRS(muzzle.position, muzzle.rotation, new Vector3(1,1,zLength)));

        impulseSource.GenerateImpulse(impulseForce);
        return ret;
    }
}

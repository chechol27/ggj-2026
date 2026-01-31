using UnityEngine;

public class PlasmaGun : Weapon
{
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
        GameServices.Get<Pool>().Spawn(VFXPrefab, out GameObject particleSystem, TransformFrame.TR(muzzle.position, muzzle.rotation));

        Ray r = new Ray(muzzle.position, muzzle.forward);
        if (Physics.Raycast(r, out RaycastHit hit, Mathf.Infinity, detectionMask))
        {
            if (hit.collider.TryGetComponent(out IDamageable<DamageMessage, DamageResponse> damageable))
            {
                DamageMessage message = new DamageMessage();
                message.value = player.Damage;
                response = damageable.TakeDamage(message);
                #pragma warning TODO Handle damage after successful shot
                return true;
            }
        }

        response = default;
        return false;
    }
}

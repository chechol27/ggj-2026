using UnityEngine;

public class PermanentBuffPickUp : MonoBehaviour
{
    [SerializeField] private string statID = "O2";
    [SerializeField] private float buffValue;
    [SerializeField] private GameObject pickupFXPrefab;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IActorComponent comp))
        {
            PlayerCharacter pc = comp.GetActor<PlayerCharacter>();
            if (pc != null)
            {
                Player p = pc;
                var buff = p.AddBuff<AddFloatBuff>(statID);
                buff.Value = buffValue;
                gameObject.SetActive(false);
                if (pickupFXPrefab != null)
                {
                    GameServices.Get<Pool>().Spawn(pickupFXPrefab, out ParticleSystem vfx, TransformFrame.T(p.CharacterPosition));
                    vfx.transform.parent = p.transform;
                    vfx.Play(true);
                }
            }
        }
    }
}

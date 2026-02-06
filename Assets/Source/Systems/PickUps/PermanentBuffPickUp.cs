using UnityEngine;

public class PermanentBuffPickUp : MonoBehaviour
{
    [SerializeField] private string statID = "O2";
    [SerializeField] private float buffValue;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IActorComponent<PlayerCharacter> comp))
        {
            Player p = comp.GetActor();
            var buff = p.AddBuff<AddFloatBuff>(statID);
            buff.Value = buffValue;
            Destroy(gameObject);
        }
    }
}

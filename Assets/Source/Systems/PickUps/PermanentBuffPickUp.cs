using UnityEngine;

public class PermanentBuffPickUp : MonoBehaviour
{
    [SerializeField] private string statID = "O2";
    [SerializeField] private float buffValue;
    
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
            }
        }
    }
}

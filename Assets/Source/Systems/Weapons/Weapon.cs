using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract bool Shoot(out DamageResponse response, Transform logicalMuzzle = null);
}

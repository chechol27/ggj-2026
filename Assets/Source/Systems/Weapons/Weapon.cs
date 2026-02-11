using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Action<float> onReload;
    public abstract bool Shoot(out DamageResponse response, out float normalizedRemainingAmmo, Transform logicalMuzzle = null);
}

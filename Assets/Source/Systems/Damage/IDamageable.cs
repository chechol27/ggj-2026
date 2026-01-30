using UnityEngine;

public interface IDamageable<TDamage, TResponse> where TDamage : struct where TResponse : struct
{
    public TResponse TakeDamage(TDamage damage);
}

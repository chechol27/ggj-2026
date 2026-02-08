using UnityEngine;

public class EnemyVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitVFX;
    [SerializeField] private ParticleSystem deadVFX;
    public void PlayHitVFX()
    {
        if (hitVFX == null)
            return;

        hitVFX.Clear();   // ← limpia partículas activas
        hitVFX.Play();    // ← siempre reinicia correctamente
    }
    public void PlaydeadVFX()
    {
        if (deadVFX == null)
            return;

        deadVFX.Clear();   // ← limpia partículas activas
        deadVFX.Play();    // ← siempre reinicia correctamente
    }

}

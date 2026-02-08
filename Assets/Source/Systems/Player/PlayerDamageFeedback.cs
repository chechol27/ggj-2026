using System.Collections;
using LitMotion;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerDamageFeedback : MonoBehaviour, IActorComponent
{
    [SerializeField] private Material feedbackMaterial;
    [SerializeField] private AnimationCurve ease;
    [SerializeField] private CinemachineImpulseSource source;

    private Player player;

    [Header("Particles")]
    [SerializeField] private ParticleSystem damageParticles;

    private void Awake()
    {
        player = GameServices.Get<Player>();
        feedbackMaterial.SetVector("_EffectParams", new Vector2(0, 0));
    }
    
    public void OnDamage()
    {
        LMotion.Create(0f, 1f, player.DamageIFrames).WithEase(ease).Bind(val => feedbackMaterial.SetVector("_EffectParams", new Vector2(val, 0)));
        source.GenerateImpulse(1);
        if (damageParticles != null)
            damageParticles.Play();
    }

    public Actor Actor { get; set; }
}

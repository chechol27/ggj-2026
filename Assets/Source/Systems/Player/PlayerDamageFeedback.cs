using System.Collections;
using LitMotion;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerDamageFeedback : MonoBehaviour, IActorComponent<PlayerCharacter>
{
    [SerializeField] private Material feedbackMaterial;
    [SerializeField] private AnimationCurve ease;
    [SerializeField] private CinemachineImpulseSource source;

    private Player player;

    private void Awake()
    {
        player = GameServices.Get<Player>();
        feedbackMaterial.SetVector("_EffectParams", new Vector2(0, 0));
    }

    IEnumerator HitStop()
    {
        yield return null;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.16f);
        Time.timeScale = 1;
    }
    
    public void OnDamage()
    {
        LMotion.Create(0f, 1f, player.DamageIFrames).WithEase(ease).Bind(val => feedbackMaterial.SetVector("_EffectParams", new Vector2(val, 0)));
        source.GenerateImpulse(1);
        StartCoroutine(HitStop());
    }

    public Actor Actor { get; set; }
}

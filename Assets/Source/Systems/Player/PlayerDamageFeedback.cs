using System.Collections;
using LitMotion;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerDamageFeedback : MonoBehaviour
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
        float deltaTime = Time.deltaTime;
        yield return null;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(deltaTime * 10);
        Time.timeScale = 1;
    }
    
    public void OnDamage()
    {
        LMotion.Create(0f, 1f, player.DamageIFrames).WithEase(ease).Bind(val => feedbackMaterial.SetVector("_EffectParams", new Vector2(val, 0)));
        source.GenerateImpulse(1);
        StartCoroutine(HitStop());
    }
}

using System;
using UnityEngine;

public class PlayerDistractController : MonoBehaviour, IActorComponent
{
    private bool isFirstTime = true;
    private Player player;
    [SerializeField] private float speedBuffDuration;
    [SerializeField] private float speedBuffAmount;
    [SerializeField] GameObject MaskPrefab;
    float useDuration = 6f;
    [SerializeField] private MainGameHUD HUD;
    

    private void Start()
    {
        player = GameServices.Get<Player>();
    }

    private void OnEnable()
    {
        useDuration = 5f;
        
        if(isFirstTime) return;
        
        ReplaceFloatBuffAnimated buff = player.AddBuff<ReplaceFloatBuffAnimated>("Speed");
        buff.Duration = speedBuffDuration;
        buff.MinValue = 15f;
        buff.MaxValue = speedBuffAmount;
    }

    private void FixedUpdate()
    {
        useDuration -= Time.deltaTime;
        if(gameObject.activeInHierarchy)
            HUD.MaskValue(useDuration*2);
                
        if (useDuration <= 0f)
        {
            LeaveRemanent();
        }
    }

    void LeaveRemanent()
    {
        GameObject go = Instantiate(MaskPrefab, transform.position, Quaternion.identity);
        GetComponentInParent<PlayerStateController>().maskCooldown =
            go.GetComponent<DistractMaskController>().lifetime * 2;
        
        GetComponentInParent<PlayerStateController>().PrematureMaskTurnOff();
    }

    public Actor Actor { get; set; }
}

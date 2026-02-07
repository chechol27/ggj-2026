using System;
using UnityEngine;
using UnityEngine.UI;

public class MainGameHUD : HUDUI
{
    [SerializeField] Slider lifeBar;
    [SerializeField] Slider o2Bar;
    [SerializeField] private Slider Mask;
    private Player player;
    
    void Start()
    {
        player = GameServices.Get<Player>();

        lifeBar.value = player.Health;
        o2Bar.value = player.O2;

        player.OnHealthChanged += UpdateHealth;
        player.OnO2Changed += UpdateO2;
    }

    void UpdateHealth(float value)
    {
        lifeBar.value = player.Health;
    }

    void UpdateO2(float value)
    {
        o2Bar.value = value;
    }

    public void MaskValue(float _value)
    {
        Mask.value = _value;
    }

    private void OnDestroy()
    {
        player.OnHealthChanged -= UpdateHealth;
        player.OnO2Changed -= UpdateO2;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider lifeBar;
    [SerializeField] Slider o2Bar;
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
}

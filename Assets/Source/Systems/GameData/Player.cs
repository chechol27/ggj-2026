using UnityEngine;

public class Player : MonoBehaviour, IGameService
{
    public PlayerConfig Config;

    private float health;
    private float o2;
    private float o2Regen;
    private float o2Deplete;
    private float damage;
    private float intelligence;
    private float speed;
    private float sprintMultiplier;
    private float fireRate;

    public float Health
    {
        get => health;
        set => health = value;
    }

    public float O2
    {
        get => o2;
        set => o2 = value;
    }

    public float O2Regen
    {
        get => o2Regen;
        set => o2Regen = value;
    }

    public float O2Deplete
    {
        get => o2Deplete;
        set => o2Deplete = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    public float Intelligence
    {
        get => intelligence;
        set => intelligence = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float SprintMultiplier
    {
        get => sprintMultiplier;
        set => sprintMultiplier = value;
    }

    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }
}

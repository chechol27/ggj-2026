using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour, IGameService, IBuffReceiver
{
    private const string CONFIG_PATH = "GameData/PlayerConfig";

    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float minO2;
    [SerializeField] private float maxO2;
    [SerializeField] private float o2;
    [SerializeField] private float o2Regen;
    [SerializeField] private float o2Deplete;
    [SerializeField] private float damage;
    [SerializeField] private float intelligence;
    [SerializeField] private float speed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float fireRate;
    private bool canMove = true;

    private float damageIFrames;
    
    public event Action<float> OnO2Changed;
    public event Action<float> OnHealthChanged;

    private float characterSpeed;
    [SerializeField] private PlayerMode currentMode = PlayerMode.Combat;

    public void Initialize()
    {
        PlayerConfig config = Resources.Load<PlayerConfig>(CONFIG_PATH);
        maxHealth = config.baseHealth;
        health = config.baseHealth;
        minO2 = config.minO2;
        maxO2 = config.maxO2;
        o2 = config.baseO2;
        o2Regen = config.baseO2Regen;
        o2Deplete = config.baseO2Deplete;
        damage = config.baseDamage;
        intelligence = config.baseIntelligence;
        speed = config.baseSpeed;
        sprintMultiplier = config.sprintMultiplier;
        fireRate = config.baseFireRate;
        damageIFrames = 0.5f;
        foreach (IBuff buff in GetComponents<IBuff>())
        {
            Destroy((Component)buff);
        }
    }
    
    private void Awake()
    {
        Initialize();
    }

    public float MaxHealth
    {
        get
        {
            float ret = maxHealth;
            foreach (IBuff buff in GetComponents<IBuff>().Where(buff => buff.StatName == "Health"))
            {
                ret = (float)buff.ModifyValue(ret);
            }

            return ret;
        }
        set => maxHealth = value;
    }

    public float Health
    {
        get => health;
        set
        {
            health = value;
            OnHealthChanged?.Invoke(health);
        }
            
    }

    public float MinO2
    {
        get => minO2;
        set => minO2 = value;
    }

    public float MaxO2
    {
        get => maxO2;
        set => maxO2 = value;
    }

    public float O2
    {
        get
        {
            float ret = o2;
            foreach (IBuff buff in GetComponents<IBuff>().Where(buff => buff.StatName == "O2"))
            {
                ret = (float)buff.ModifyValue(ret);
            }

            return ret;
        }
        set
        {
            o2 = Mathf.Clamp(value, MinO2, MaxO2);
            OnO2Changed?.Invoke(o2);
        }
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
        get
        {
            float ret = speed;
            foreach (IBuff buff in GetComponents<IBuff>().Where(buff => buff.StatName == "Speed"))
            {
                ret = (float)buff.ModifyValue(ret);
            }

            return ret;
        }
        set => speed = value;
    }

    public float SprintMultiplier
    {
        get => sprintMultiplier;
        set => sprintMultiplier = value;
    }

    public float FireRate
    {
        get
        {
            float ret = fireRate;
            foreach (IBuff buff in GetComponents<IBuff>().Where(buff => buff.StatName == "FireRate"))
            {
                ret = (float)buff.ModifyValue(ret);
            }

            return ret;
        }
        set => fireRate = value;
    }

    public PlayerMode CurrentMode
    {
        get => currentMode;
        set => currentMode = value;
    }

    public Vector3 CharacterPosition { get; set; }

    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    public float CharacterSpeed
    {
        get => characterSpeed;
        set => characterSpeed = value;
    }

    public float DamageIFrames
    {
        get => damageIFrames;
        set => damageIFrames = value;
    }

    public TBuff AddBuff<TBuff>(string statName) where TBuff : Component, IBuff
    {
        TBuff buff = gameObject.AddComponent<TBuff>();
        buff.StatName = statName;
        return buff;
    }
}

using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IGameService
{
    private const string CONFIG_PATH = "GameData/PlayerConfig";

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
    
    private void Awake()
    {
        PlayerConfig config = Resources.Load<PlayerConfig>(CONFIG_PATH); 
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
    }

    public float Health
    {
        get => health;
        set => health = value;
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
        get => o2;
        set
        {
            o2 = Mathf.Clamp(value, MinO2, MaxO2);
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
    
    public Vector3 CharacterPosition { get; set; }
}

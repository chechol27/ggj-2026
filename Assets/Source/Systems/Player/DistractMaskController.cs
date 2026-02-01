using System;
using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

public class DistractMaskController : MonoBehaviour
{
    [SerializeField] public float lifetime;
    private RangedEnemyMovement[] activeRange;
    private MeleeEnemyMovement[] activeMeelee;

    private void Start()
    {
        activeMeelee = FindObjectsByType<MeleeEnemyMovement>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        activeRange = FindObjectsByType<RangedEnemyMovement>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        
        foreach (MeleeEnemyMovement activeEnemy in activeMeelee)
        {
            activeEnemy.ChangeTarget(transform.position, lifetime, true);
        }
        
        foreach (RangedEnemyMovement activeEnemy in activeRange)
        {
            activeEnemy.ChangeTarget(transform.position, lifetime, true);
        }
        
        Invoke(nameof(MyDestroy), lifetime);
        LMotion.Create(transform.eulerAngles, transform.eulerAngles + new Vector3(0, 720, 0),
            lifetime).BindToEulerAngles(transform);

    }

    private void MyDestroy()
    {
        Destroy(gameObject);
    }
}

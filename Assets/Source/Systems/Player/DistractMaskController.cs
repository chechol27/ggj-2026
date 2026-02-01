using System;
using UnityEngine;

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
    }

    private void MyDestroy()
    {
        Destroy(gameObject);
    }
}

using UnityEngine.Events;

public class MeleeEnemyCharacter : Actor
{
    public UnityEvent onDeath;
    public UnityEvent onAttackStarted;
    public UnityEvent onAttackPerformed;

    void ForwardInternalEvents()
    {
        GetActorComponent<EnemyHealth>()?.onDeath.AddListener(onDeath.Invoke);
        GetActorComponent<MeleeEnemyAttacks>()?.onAttackStarted.AddListener(onAttackStarted.Invoke);
    }
    
    protected override void Awake()
    {
        base.Awake();
        ForwardInternalEvents();
    }
}

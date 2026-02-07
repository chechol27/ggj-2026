using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A root class to group all components in hierarchy that make up the structure of the actor's behavior.
/// Provides a way to:
/// 1. Propagate reference resolution between internal components, this root class and other classes not contained in this actor's system
/// 2. Observe internal events and propagate their behaviors outside of this actor's systems
/// </summary>
[DefaultExecutionOrder(-2)]
public abstract class Actor : MonoBehaviour
{
    protected List<IActorComponent> components = new List<IActorComponent>();
    
    protected virtual void Awake()
    {
        components.AddRange(GetComponentsInChildren<IActorComponent>());
        foreach (var actorComponent in components)
        {
            actorComponent.Actor = this;
        }
    }

    public TActorComp GetActorComponent<TActorComp>() where TActorComp : Component, IActorComponent
    {
        return (TActorComp)components.First(x => x is TActorComp);
    }
}

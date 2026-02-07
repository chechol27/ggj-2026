using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

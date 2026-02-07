using System.ComponentModel;

public interface IActorComponent
{
    public Actor Actor { get; set; }

    public TActor GetActor<TActor>() where TActor : Actor
    {
        return Actor as TActor;
    }
}

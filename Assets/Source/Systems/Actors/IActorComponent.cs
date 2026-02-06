public interface IActorComponent
{
    public Actor Actor { get; set; }
}

public interface IActorComponent<TActor> : IActorComponent where TActor : Actor
{
    public TActor GetActor() => (TActor)Actor;
}

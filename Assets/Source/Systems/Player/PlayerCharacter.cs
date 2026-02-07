public class PlayerCharacter : Actor
{
    protected override void Awake()
    {
        base.Awake();
    }

    public static implicit operator Player(PlayerCharacter character)
    {
        return GameServices.Get<Player>();
    }
}

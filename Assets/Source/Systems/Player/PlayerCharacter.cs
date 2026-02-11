using System;

public class PlayerCharacter : Actor
{
    protected override void Awake()
    {
        base.Awake();
        GameServices.Get<Player>().Character = this;
        GetActorComponent<PlayerShoot>().onAmmoChanged.AddListener(val => onWeaponAmmoChanged?.Invoke(val));
    }

    public static implicit operator Player(PlayerCharacter character)
    {
        return GameServices.Get<Player>();
    }

    public Action<float> onWeaponAmmoChanged;
}

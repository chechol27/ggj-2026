using UnityEngine;

public interface IBuffReceiver
{
    public TBuff AddBuff<TBuff>(string statName) where TBuff : Component, IBuff;
}

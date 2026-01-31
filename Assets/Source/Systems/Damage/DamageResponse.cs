public enum DamageResult
{
    Damaged,
    Immune,
    Cured,
    Dead
}

public struct DamageResponse
{
    public float receivedDamage;
    public DamageResult result;
}
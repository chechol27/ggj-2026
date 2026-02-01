using UnityEngine;

public class AddFloatBuff : MonoBehaviour, IBuff
{
    [field:SerializeField] public string StatName { get; set; }
    public float Value { get; set; }
    public object ModifyValue(object value)
    {
        if (value is float f)
        {
            return f + Value;
        }

        return value;
    }
}

public interface IBuff
{
    public string StatName { get; set; }
    public object ModifyValue(object value);
}
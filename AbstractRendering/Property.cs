namespace AbstractRendering;

public class Property
{
    public float Value;

    public Property(float value)
    {
        Value = value;
    }

    public static implicit operator float(Property property) => property.Value;
    public static implicit operator Property(float value) => new (value);
    
    
    
}
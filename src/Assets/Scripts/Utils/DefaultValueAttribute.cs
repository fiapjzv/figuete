using System;

[AttributeUsage(AttributeTargets.Field)]
public class DefaultValueAttribute : Attribute
{
    public float Value { get; }

    public DefaultValueAttribute(float value)
    {
        Value = value;
    }
}

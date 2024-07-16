using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ModifierType { Flat = 100, Additive = 200, Multiplicative = 300 }
public readonly struct Modifier 
{
    public ModifierType Type { get; }
    public object Source { get; }

    private readonly float _value;

    public Modifier(float value, ModifierType modifierType, object source=null)
    {
        _value = value;
        Type = modifierType;
        Source = source;
    }

    public override string ToString()
    {
        return $"Value: {_value}, Type: {Type}, Source: {Source}";
    }
    public static implicit operator float(Modifier modifier) => modifier._value;
}

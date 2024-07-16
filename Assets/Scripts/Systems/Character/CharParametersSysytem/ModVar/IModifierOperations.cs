using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifierOperations
{
    public void AddModifier(Modifier modifier);
    public bool RemoveModifier(Modifier modifier);
    public float CalculateValue(float baseValue, float currentValue);
    public List<Modifier> GetAllModifiers();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicativeModifierOperations : ModifierOperatorBase
{
    public override float CalculateValue(float baseValue, float currentValue)
    {
        //Debug.Log("Multiplicative moifier calculated");
        float toRet = currentValue;
        foreach (var modifier in modifiers) toRet += toRet * modifier;
        return toRet - currentValue;
    }
}

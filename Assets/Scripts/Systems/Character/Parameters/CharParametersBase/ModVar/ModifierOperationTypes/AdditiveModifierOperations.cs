using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdditiveModifierOperations : ModifierOperatorBase
{
    public override float CalculateValue(float baseValue, float currentValue)
    {
        //Debug.Log("Additive modifier calculated");
        return baseValue * modifiers.Sum(m=>(float)m);
    }
}

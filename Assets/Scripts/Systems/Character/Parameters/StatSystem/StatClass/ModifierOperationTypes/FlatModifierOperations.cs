using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlatModifierOperations : ModifierOperatorBase
{ 
    public override float CalculateValue(float baseValue, float currentValue)
    {
        //Debug.Log("Flat moifier calculated");
        return modifiers.Sum(m=>(float)m); // !!! Перевіврити чи працює
    }
}

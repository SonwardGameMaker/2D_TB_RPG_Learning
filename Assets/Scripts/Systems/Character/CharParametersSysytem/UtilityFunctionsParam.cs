using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModValueCalculateLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets);

public delegate (float, ModifierType) ModifierBaseCreation(CharParameterBase affector);
public delegate List<Modifier> ModifierBaseCreationList(List<CharParameterBase> affectors);
public delegate ModVar GetSomeValue (CharParameterBase stat);
public delegate float GetSomeValueFloat (CharParameterBase stat);

public static class UtilityFunctionsParam
{
    // 
    public static void ParamModifyingValueSelectively(
        ref List<CharParameterBase> affectors, 
        ref List<CharParameterBase> targets,
        GetSomeValueFloat GetAffectorValue,
        GetSomeValue GetTargetValue,
        ModifierBaseCreation CreateModifierBase)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            CharParameterBase currentAffecter = affectors[0];
            foreach (CharParameterBase affector in affectors)
            {
                GetTargetValue(targets[i]).TryRemoveAllModifiersOf(affector);
                if (GetAffectorValue(affector) > GetAffectorValue(currentAffecter))
                {
                    currentAffecter = affector;
                }
            }
            (float, ModifierType) result = CreateModifierBase(currentAffecter);
            GetTargetValue(targets[i]).AddModifier(new Modifier(result.Item1, result.Item2, currentAffecter));
        }
    }
    public static void ParamModifyingValueSimultaneously(
        ref List<CharParameterBase> affectors,
        ref List<CharParameterBase> targets,
        GetSomeValueFloat GetAffectorValue,
        GetSomeValue GetTargetValue,
        ModifierBaseCreationList CreateModifierBaseList)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            List<Modifier> result = CreateModifierBaseList(affectors);
            foreach (Modifier modifier in result)
            {
                GetTargetValue(targets[i]).TryRemoveAllModifiersOf(modifier.Source);
            }
            foreach (Modifier modifier in result)
            {
                GetTargetValue(targets[i]).AddModifier(modifier);
            }
        }
    }


    public static float GetMinValFloat(CharParameterBase stat)
    {
        if (stat is IMinValModifiable modVal)
                return modVal.MinValue.RealValue;
        if (stat is IMinValUnmod unmod)
            return unmod.MinValue;
        else throw new Exception("Cannot get targets min value");
    }
    public static float GetCurrentValFloat(CharParameterBase stat)
    {
        if (stat is ICurrValModifiable modVal)
            return modVal.CurrentValue.RealValue;
        if (stat is ICurrValUnmod unmod)
            return unmod.CurrentValue;
        else throw new Exception("Cannot get targets current value");
    }
    public static float GetMaxValFloat(CharParameterBase stat)
    {
        if (stat is IMaxValModifiable modVal)
            return modVal.MaxValue.RealValue;
        if (stat is IMaxValUnmod unmod)
            return unmod.MaxValue;
        else throw new Exception("Cannot get targets max value");
    }
    public static ModVar GetMinVal(CharParameterBase stat)
    {
        if (stat is IMinValModifiable modVal)
            return modVal.MinValue;
        else throw new Exception("Cannot modify targets min value");
    }
    public static ModVar GetCurrentVal(CharParameterBase stat)
    {
        if (stat is ICurrValModifiable modVal)
            return modVal.CurrentValue;
        else throw new Exception("Cannot modify targets current value");
    }
    public static ModVar GetMaxVal(CharParameterBase stat)
    {
        if (stat is IMaxValModifiable modVal)
            return modVal.MaxValue;
        else throw new Exception("Cannot modify targets max value");
    }

    

}

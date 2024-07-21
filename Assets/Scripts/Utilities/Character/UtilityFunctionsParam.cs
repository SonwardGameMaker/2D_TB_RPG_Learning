using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModValueCalculateLogic(ref List<CharParameterBase> affectors, ref List<CharParameterBase> targets);

public delegate (float, ModifierType) ModifierBaseCreation(CharParameterBase affector);
public delegate List<Modifier> ModifierBaseCreationList(List<CharParameterBase> affectors);
public delegate ConcreteValueWorker ValueWorker(CharParameterBase stat);
public delegate float GetSomeValueFloat (CharParameterBase stat);

public static class UtilityFunctionsParam
{
    #region main funtions
    public static void AffectorsCompareTargetsEvery(
        ref List<CharParameterBase> affectors, 
        ref List<CharParameterBase> targets,
        GetSomeValueFloat GetAffectorValue,
        ValueWorker ValueWorker,
        ModifierBaseCreation CreateModifierBase)
    {
        CharParameterBase currentAffector = affectors[0];
        foreach (CharParameterBase affector in affectors)
            if (GetAffectorValue(affector) > GetAffectorValue(currentAffector))
                currentAffector = affector;
            
        for (int i = 0; i < targets.Count; i++)
        {
            ConcreteValueWorker worker = ValueWorker(targets[i]);
            worker.TryRemoveAllModifiersOf(currentAffector);
            (float, ModifierType) result = CreateModifierBase(currentAffector);
            ValueWorker(targets[i]).AddModifier(new Modifier(result.Item1, result.Item2, currentAffector));
        }
    }
    public static void AffectorsAllTargetsEvery(
        ref List<CharParameterBase> affectors,
        ref List<CharParameterBase> targets,
        GetSomeValueFloat GetAffectorValue,
        ValueWorker ValueWorker,
        ModifierBaseCreationList CreateModifierBaseList)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            ConcreteValueWorker worker = ValueWorker(targets[i]);
            List<Modifier> result = CreateModifierBaseList(affectors);
            foreach (Modifier modifier in result)
            {
                worker.TryRemoveAllModifiersOf(modifier.Source);
            }
            foreach (Modifier modifier in result)
            {
                worker.AddModifier(modifier);
            }
        }
    }
    #endregion

    #region GetValue functions
    public static float GetMinValFloat(CharParameterBase stat)
    {
        if (stat is IMinValModifiable modVal)
                return modVal.MinValue;
        if (stat is IMinValUnmod unmod)
            return unmod.MinValue;
        else throw new Exception("Cannot get targets min value");
    }
    public static float GetCurrentValFloat(CharParameterBase stat)
    {
        if (stat is ICurrValModifiable modVal)
            return modVal.CurrentValue;
        if (stat is ICurrValUnmod unmod)
            return unmod.CurrentValue;
        else throw new Exception("Cannot get targets current value");
    }
    public static float GetMaxValFloat(CharParameterBase stat)
    {
        if (stat is IMaxValModifiable modVal)
            return modVal.MaxValue;
        if (stat is IMaxValUnmod unmod)
            return unmod.MaxValue;
        else throw new Exception("Cannot get targets max value");
    }

    public static MaxValueWorker GetMaxValueMod(CharParameterBase stat) => new MaxValueWorker(stat);
    public static MinValueWorker GetMinValueMod(CharParameterBase stat) => new MinValueWorker(stat);
    public static CurrentValueWorker GetCurrValueMod(CharParameterBase stat) => new CurrentValueWorker(stat);
    #endregion

    #region CheckObject functions
    public static IMaxValModifiable IsMaxValueMod(CharParameterBase obj)
    {
        if (obj is IMaxValModifiable modVal) return modVal;
        else throw new Exception($"Max value of parameter do not implement {typeof(IMaxValModifiable)}");
    }
    public static IMinValModifiable IsMinValueMod(CharParameterBase obj)
    {
        if (obj is IMinValModifiable modVal) return modVal;
        else throw new Exception($"Max value of parameter do not implement {typeof(IMinValModifiable)}");
    }
    public static ICurrValModifiable IsCurrValueMod(CharParameterBase obj)
    {
        if (obj is ICurrValModifiable modVal) return modVal;
        else throw new Exception($"Max value of parameter do not implement {typeof(ICurrValModifiable)}");
    }
    #endregion
}

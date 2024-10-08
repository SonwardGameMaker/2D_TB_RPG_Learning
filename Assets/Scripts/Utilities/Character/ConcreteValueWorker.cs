using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConcreteValueWorker
{
    //public ConcreteValueWorker(CharParameterBase param) { }

    public abstract void AddModifier(Modifier modifier);
    public abstract IReadOnlyList<Modifier> GetModifiers();
    public abstract IReadOnlyList<Modifier> GetModifiers(ModifierType modifierType);
    public abstract bool TryRemoveModifier(Modifier modifier);
    public abstract bool TryRemoveAllModifiersOf(object source);
}


public class MaxValueWorker : ConcreteValueWorker
{
    IMaxValModifiable _maxValModifiable;
    public MaxValueWorker(CharParameterBase parameter)
    {
        if (parameter is IMaxValModifiable) _maxValModifiable = (IMaxValModifiable)parameter;
        else throw new Exception($"Object is not implement {typeof(IMaxValModifiable)} interface");
    }

    public override void AddModifier(Modifier modifier) => _maxValModifiable.AddMaxValueModifier(modifier);
    public override IReadOnlyList<Modifier> GetModifiers() => _maxValModifiable.GetMaxValueModifiers();
    public override IReadOnlyList<Modifier> GetModifiers(ModifierType modifierType) => _maxValModifiable.GetMaxValueModifiers(modifierType);
    public override bool TryRemoveModifier(Modifier modifier) => _maxValModifiable.TryRemoveMaxValueModifier(modifier);
    public override bool TryRemoveAllModifiersOf(object source) => _maxValModifiable.TryRemoveMaxValueAllModifiersOf(source);
}

public class MinValueWorker : ConcreteValueWorker
{
    IMinValModifiable _minValModifiable;
    public MinValueWorker(CharParameterBase parameter)
    {
        if (parameter is IMinValModifiable) _minValModifiable = (IMinValModifiable)parameter;
        else throw new Exception($"Object is not implement {typeof(IMinValModifiable)} interface");
    }

    public override void AddModifier(Modifier modifier) => _minValModifiable.AddMinValueModifier(modifier);
    public override IReadOnlyList<Modifier> GetModifiers() => _minValModifiable.GetMinValueModifiers();
    public override IReadOnlyList<Modifier> GetModifiers(ModifierType modifierType) => _minValModifiable.GetMinValueModifiers(modifierType);
    public override bool TryRemoveModifier(Modifier modifier) => _minValModifiable.TryRemoveMinValueModifier(modifier);
    public override bool TryRemoveAllModifiersOf(object source) => _minValModifiable.TryRemoveMinValueAllModifiersOf(source);
}

public class CurrentValueWorker : ConcreteValueWorker
{
    ICurrValModifiable _currValModifiable;
    public CurrentValueWorker(CharParameterBase parameter)
    {
        if (parameter is ICurrValModifiable) _currValModifiable = (ICurrValModifiable)parameter;
        else throw new Exception($"Object is not implement {typeof(ICurrValModifiable)} interface");
    }

    public override void AddModifier(Modifier modifier) => _currValModifiable.AddCurrentValueModifier(modifier);
    public override IReadOnlyList<Modifier> GetModifiers() => _currValModifiable.GetCurrentValueModifiers();
    public override IReadOnlyList<Modifier> GetModifiers(ModifierType modifierType) => _currValModifiable.GetCurrentValueModifiers(modifierType);
    public override bool TryRemoveModifier(Modifier modifier) => _currValModifiable.TryRemoveCurrentValueModifier(modifier);
    public override bool TryRemoveAllModifiersOf(object source) => _currValModifiable.TryRemoveCurrentValueAllModifiersOf(source);
}

public class TrashholdValueWorker : ConcreteValueWorker
{
    IArmorTrashholdMod _armorTrashholdMod;

    public TrashholdValueWorker(CharParameterBase parameter)
    {
        if (parameter is IArmorTrashholdMod) _armorTrashholdMod = (IArmorTrashholdMod)parameter;
        else throw new Exception($"Object is not implement {typeof(IArmorTrashholdMod)} interface");
    }

    public override void AddModifier(Modifier modifier) => _armorTrashholdMod.AddTrashholdValueModifier(modifier);
    public override IReadOnlyList<Modifier> GetModifiers() => _armorTrashholdMod.GetTrashholdValueModifiers();
    public override IReadOnlyList<Modifier> GetModifiers(ModifierType modifierType) => _armorTrashholdMod.GetTrashholdValueModifiers(modifierType);
    public override bool TryRemoveModifier(Modifier modifier) => _armorTrashholdMod.TryRemoveTrashholdValueModifier(modifier);
    public override bool TryRemoveAllModifiersOf(object source) => _armorTrashholdMod.TryRemoveTrashholdValueAllModifiersOf(source);
}

public class MitigationValueWorker : ConcreteValueWorker
{
    IArmorMitigationMod _armorMitigationMod;

    public MitigationValueWorker(CharParameterBase parameter)
    {
        if (parameter is IArmorMitigationMod) _armorMitigationMod = (IArmorMitigationMod)parameter;
        else throw new Exception($"Object is not implement {typeof(IArmorMitigationMod)} interface");
    }

    public override void AddModifier(Modifier modifier) => _armorMitigationMod.AddMitigationValueModifier(modifier);
    public override IReadOnlyList<Modifier> GetModifiers() => _armorMitigationMod.GetMitigationValueModifiers();
    public override IReadOnlyList<Modifier> GetModifiers(ModifierType modifierType) => _armorMitigationMod.GetMitigationValueModifiers(modifierType);
    public override bool TryRemoveModifier(Modifier modifier) => _armorMitigationMod.TryRemoveMitigationValueModifier(modifier);
    public override bool TryRemoveAllModifiersOf(object source) => _armorMitigationMod.TryRemoveMitigationValueAllModifiersOf(source);
}

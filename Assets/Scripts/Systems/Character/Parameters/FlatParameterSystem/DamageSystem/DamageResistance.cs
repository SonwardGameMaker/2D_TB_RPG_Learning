using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageResistance : CharParameterBase, IArmorTrashholdMod, IArmorMitigationMod
{
    protected const float DEFAULT_TRASHHOLD_VALUE = 0.0f;
    protected const float DEFAULT_MITIGATION_VALUE = 0.0f;
    protected new const string DEFAULT_NAME = "Default_DamageResistance_Name";

    private DamageType _damageResistanceType;
    private ModVar _trashhold;
    private ModVar _mitigation;

    public DamageResistance(DamageType damageResistanceType, float trashhold, float mitigation, string name)
    {
        _damageResistanceType = damageResistanceType;
        _trashhold.BaseValue = trashhold;
        _mitigation.BaseValue = mitigation;
        _name = name;
    }
    public DamageResistance(DamageType damageResistanceType, float trashhold, float mitigation) 
        : this(damageResistanceType, trashhold, mitigation, $"{damageResistanceType.ToString()}_Resistance") { }
    public DamageResistance(DamageType damageResistanceType, float trashhold)
        : this(damageResistanceType, trashhold, DEFAULT_MITIGATION_VALUE, $"{damageResistanceType.ToString()}_Resistance") { }
    public DamageResistance(DamageType damageResistanceType)
        : this(damageResistanceType, DEFAULT_TRASHHOLD_VALUE , DEFAULT_MITIGATION_VALUE, $"{damageResistanceType.ToString()}_Resistance") { }
    public DamageResistance()
        : this(DamageType.Mechanical, DEFAULT_TRASHHOLD_VALUE, DEFAULT_MITIGATION_VALUE, $"{DamageType.Mechanical.ToString()}_Resistance") { }
    
    public DamageType DamageType
    {
        get => _damageResistanceType;
        private set => _damageResistanceType = value;
    }
    public float Trashhold { get => _trashhold.RealValue; }
    public float TrashholdBase 
    {
        get => _trashhold.BaseValue;
        private set => _trashhold.BaseValue = value;
    }
    public float Mitigation { get => _mitigation.RealValue; }
    public float MitigationBase 
    { 
        get => _mitigation.BaseValue;
        private set => _mitigation.BaseValue = value;
    }


    // Trashhold modifiers
    public void AddTrashholdValueModifier(Modifier modifier) => _trashhold.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetTrashholdValueModifiers() => _trashhold.GetModifiers();
    public IReadOnlyList<Modifier> GetTrashholdValueModifiers(ModifierType modifierType) => _trashhold.GetModifiers(modifierType);
    public bool TryRemoveTrashholdValueModifier(Modifier modifier) => _trashhold.TryRemoveModifier(modifier);
    public bool TryRemoveTrashholdValueAllModifiersOf(object source) => _trashhold.TryRemoveAllModifiersOf(source);

    // Mitigation modifiers
    public void AddMitigationValueModifier(Modifier modifier) => _mitigation.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetMitigationValueModifiers() => _mitigation.GetModifiers();
    public IReadOnlyList<Modifier> GetMitigationValueModifiers(ModifierType modifierType) => _mitigation.GetModifiers(modifierType);
    public bool TryRemoveMitigationValueModifier(Modifier modifier) => _mitigation.TryRemoveModifier(modifier);
    public bool TryRemoveMitigationValueAllModifiersOf(object source) => _mitigation.TryRemoveAllModifiersOf(source);
}

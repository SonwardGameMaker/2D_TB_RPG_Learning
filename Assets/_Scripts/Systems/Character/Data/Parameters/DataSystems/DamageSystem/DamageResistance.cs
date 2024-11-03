using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DamageResistance : CharParameterBase, IArmorTrashholdMod, IArmorMitigationMod
{
    protected const float DEFAULT_TRASHHOLD_VALUE = 0.0f;
    protected const float DEFAULT_MITIGATION_VALUE = 0.0f;
    protected new const string DEFAULT_NAME = "Default_DamageResistance_Name";

    [SerializeField] private DamageType _damageResistanceType;
    [SerializeField] private ModVar _trashhold;
    [SerializeField] private ModVar _mitigation;

    #region events
    public event Action TrashholdChanged;
    public event Action MitigationChanged;
    #endregion

    public DamageResistance(DamageType damageResistanceType, float trashhold, float mitigation, string name)
    {
        _damageResistanceType = damageResistanceType;
        _trashhold = new ModVar(trashhold);
        _mitigation = new ModVar(mitigation);
        _name = name;

        _trashhold.ValueChanged += OnTrashlodChanged;
        _mitigation.ValueChanged += OnMitigationChanged;
    }
    #region derivative constructors and destructor
    public DamageResistance(DamageType damageResistanceType, float trashhold, float mitigation) 
        : this(damageResistanceType, trashhold, mitigation, $"{damageResistanceType.ToString()}_Resistance") { }
    public DamageResistance(DamageType damageResistanceType, float trashhold)
        : this(damageResistanceType, trashhold, DEFAULT_MITIGATION_VALUE, $"{damageResistanceType.ToString()}_Resistance") { }
    public DamageResistance(DamageType damageResistanceType)
        : this(damageResistanceType, DEFAULT_TRASHHOLD_VALUE , DEFAULT_MITIGATION_VALUE, $"{damageResistanceType.ToString()}_Resistance") { }
    public DamageResistance()
        : this(DamageType.Mechanical, DEFAULT_TRASHHOLD_VALUE, DEFAULT_MITIGATION_VALUE, $"{DamageType.Mechanical.ToString()}_Resistance") { }

    public DamageResistance(DamageResistance damageResistance)
    {
        _damageResistanceType = damageResistance._damageResistanceType;
        _trashhold = new ModVar(damageResistance._trashhold);
        _mitigation = new ModVar (damageResistance._mitigation);
    }
    ~DamageResistance()
    {
        _trashhold.ValueChanged -= OnTrashlodChanged;
        _mitigation.ValueChanged -= OnMitigationChanged;
    }
    #endregion

    #region properties
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
    #endregion

    #region modifiers operations
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
    #endregion

    #region events invokers
    public void OnTrashlodChanged() => TrashholdChanged?.Invoke();
    public void OnMitigationChanged() => MitigationChanged?.Invoke();
    #endregion

    #region external operations
    public override void SubscribeToAll(Action action)
    {
        TrashholdChanged += action;
        MitigationChanged += action;
    }

    public override void UnsubscribeToAll(Action action)
    {
        TrashholdChanged -= action;
        MitigationChanged -= action;
    }
    #endregion
}

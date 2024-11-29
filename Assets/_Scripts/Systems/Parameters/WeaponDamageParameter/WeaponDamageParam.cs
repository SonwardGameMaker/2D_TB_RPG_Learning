using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponDamageParam : CharParameterBase, IMinValModifiable, IMaxValModifiable
{
    [SerializeField] protected ModVar _minValue;
    [SerializeField] protected ModVar _maxValue;
    [SerializeField] protected ModVar _damgeMod;

    #region events
    public event Action MinValChanged;
    public event Action MaxValChanged;
    #endregion

    #region constructors and destructor
    public WeaponDamageParam(string name, float maxValue, float minValue, float damageValueModifier)
    {
        _name = name;
        _minValue = new ModVar(minValue);
        _maxValue = new ModVar(maxValue);
        _damgeMod = new ModVar(0);

        _minValue.IsUpperBounded = true;
        _minValue.UpperBound = _maxValue.RealValue;
        _maxValue.IsLowerBounded = true;
        _maxValue.LowerBound = _minValue.RealValue;

        _minValue.ValueChanged += HandleMinValEvents;
        _maxValue.ValueChanged += HandleMaxValEvents;
    }
    public WeaponDamageParam(string name, float maxValue, float minValue) : this(name, maxValue, minValue, maxValue) { }
    /// <summary>
    /// Default min value is 0. Default current value is max value
    /// </summary>
    public WeaponDamageParam(string name, float maxValue) : this(name, maxValue, DEFAULT_MIN_VALUE, maxValue) { }
    /// <summary>
    /// Default min value is 0 and default max value is 30. Default current value is max value
    /// </summary>
    public WeaponDamageParam(string name) : this(name, DEFAULT_MAX_VALUE, DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE) { }
    public WeaponDamageParam() : this(DEFAULT_NAME, DEFAULT_MAX_VALUE, DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE) { }
    ~WeaponDamageParam()
    {
        _maxValue.ValueChanged -= HandleMaxValEvents;
    }
    #endregion

    #region properties
    public float MinValue
    {
        get => _minValue.RealValue;
    }
    public float MinValueBase
    {
        get => _minValue.RealValue;
        set
        {
            if (value > _maxValue.RealValue) _minValue.BaseValue = _maxValue.RealValue;
            else _minValue.BaseValue = value;
        }
    }
    public float MaxValue { get => _maxValue.RealValue; }
    public float MaxValueBase
    {
        get => _maxValue.BaseValue;
        set
        {
            if (value < _minValue.RealValue) _maxValue.BaseValue = _minValue.RealValue;
            else _maxValue.BaseValue = value;
        }
    }
    #endregion

    #region external interactions
    public float CalculateDamage()
        => UnityEngine.Random.Range(_minValue.RealValue, _maxValue.RealValue) + _damgeMod.RealValue;

    public override void SubscribeToAll(Action action)
    {
        MinValChanged += action;
        MaxValChanged += action;
    }

    public override void UnsubscribeToAll(Action action)
    {
        MinValChanged -= action;
        MaxValChanged -= action;
    }

    public override string ToString()
    {
        return "Stat can have values from " + _minValue + " to " + _maxValue.RealValue + ";";
    }
    #endregion

    #region modifers operations
    // Min value
    public void AddMinValueModifier(Modifier modifier) => _minValue.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetMinValueModifiers() => _minValue.GetModifiers();
    public IReadOnlyList<Modifier> GetMinValueModifiers(ModifierType modifierType) => _minValue.GetModifiers(modifierType);
    public bool TryRemoveMinValueModifier(Modifier modifier) => _minValue.TryRemoveModifier(modifier);
    public bool TryRemoveMinValueAllModifiersOf(object source) => _minValue.TryRemoveAllModifiersOf(source);

    // Max value
    public void AddMaxValueModifier(Modifier modifier) => _maxValue.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetMaxValueModifiers() => _maxValue.GetModifiers();
    public IReadOnlyList<Modifier> GetMaxValueModifiers(ModifierType modifierType) => _maxValue.GetModifiers(modifierType);
    public bool TryRemoveMaxValueModifier(Modifier modifier) => _maxValue.TryRemoveModifier(modifier);
    public bool TryRemoveMaxValueAllModifiersOf(object source) => _maxValue.TryRemoveAllModifiersOf(source);

    // Damage modifier value
    public void AddDamageValueModifier(Modifier modifier) => _damgeMod.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetDamageValueModifiers() => _damgeMod.GetModifiers();
    public IReadOnlyList<Modifier> GetDamageValueModifiers(ModifierType modifierType) => _damgeMod.GetModifiers(modifierType);
    public bool TryRemoveDamageValueModifier(Modifier modifier) => _damgeMod.TryRemoveModifier(modifier);
    public bool TryRemoveDamageValueAllModifiersOf(object source) => _damgeMod.TryRemoveAllModifiersOf(source);
    #endregion

    #region event handdlers
    protected virtual void HandleMinValEvents()
    {
        MinValChanged?.Invoke();
    }
    protected virtual void HandleMaxValEvents()
    {
        MaxValChanged?.Invoke();
    }
    #endregion
}

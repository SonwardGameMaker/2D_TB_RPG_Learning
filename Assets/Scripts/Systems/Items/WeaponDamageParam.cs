using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageParam : CharParameterBase, IMinValModifiable, ICurrValModifiable, IMaxValModifiable
{
    [SerializeField] protected ModVar _minValue;
    [SerializeField] protected ModVar _maxValue;
    [SerializeField] protected ModVar _currnetValueMod;

    #region constructors and destructor
    public WeaponDamageParam(string name, float maxValue, float minValue, float currentValue)
    {
        _name = name;
        _minValue = new ModVar(minValue);
        _maxValue = new ModVar(maxValue);

        _minValue.IsUpperBounded = true;
        _minValue.UpperBound = _maxValue.RealValue;
        _maxValue.IsLowerBounded = true;
        _maxValue.LowerBound = _minValue.RealValue;

        _minValue.ValueChanged += HandleMinValEvents;
        _currnetValueMod.ValueChanged += HandleCurrValModEvents;
        _maxValue.ValueChanged += HandleMaxValEvents;
    }
    public WeaponDamageParam(string name, float maxValue, float minValue) : this(name, maxValue, minValue, maxValue) { }
    /// <summary>
    /// Default min value is 0. Default current value is max value
    /// </summary>
    public WeaponDamageParam(string name, float maxValue) : this(name, DEFAULT_MIN_VALUE, maxValue, maxValue) { }
    /// <summary>
    /// Default min value is 0 and default max value is 30. Default current value is max value
    /// </summary>
    public WeaponDamageParam(string name) : this(name, DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE, DEFAULT_MAX_VALUE) { }
    public WeaponDamageParam() : this(DEFAULT_NAME, DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE, DEFAULT_MAX_VALUE) { }
    ~WeaponDamageParam() // дивна поведінка, перевірити
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
    public float CurrentValue
    {
        get => UnityEngine.Random.Range(MinValue, MaxValue) + _currnetValueMod.RealValue;
    }
    public float CurrentValueBase
    {
        get => UnityEngine.Random.Range(MinValue, MaxValue);
        set { } // this do nothing
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

    #region modifers operations
    // Min value
    public void AddMinValueModifier(Modifier modifier) => _minValue.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetMinValueModifiers() => _minValue.GetModifiers();
    public IReadOnlyList<Modifier> GetMinValueModifiers(ModifierType modifierType) => _minValue.GetModifiers(modifierType);
    public bool TryRemoveMinValueModifier(Modifier modifier) => _minValue.TryRemoveModifier(modifier);
    public bool TryRemoveMinValueAllModifiersOf(object source) => _minValue.TryRemoveAllModifiersOf(source);

    // Currnet value modifier
    public void AddCurrentValueModifier(Modifier modifier) => _currnetValueMod.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetCurrentValueModifiers() => _currnetValueMod.GetModifiers();
    public IReadOnlyList<Modifier> GetCurrentValueModifiers(ModifierType modifierType) => _currnetValueMod.GetModifiers(modifierType);
    public bool TryRemoveCurrentValueModifier(Modifier modifier) => _currnetValueMod.TryRemoveModifier(modifier);
    public bool TryRemoveCurrentValueAllModifiersOf(object source) => _currnetValueMod.TryRemoveAllModifiersOf(source);
    // Max value
    public void AddMaxValueModifier(Modifier modifier) => _maxValue.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetMaxValueModifiers() => _maxValue.GetModifiers();
    public IReadOnlyList<Modifier> GetMaxValueModifiers(ModifierType modifierType) => _maxValue.GetModifiers(modifierType);
    public bool TryRemoveMaxValueModifier(Modifier modifier) => _maxValue.TryRemoveModifier(modifier);
    public bool TryRemoveMaxValueAllModifiersOf(object source) => _maxValue.TryRemoveAllModifiersOf(source);
    #endregion

    #region events
    public event Action MinValChanged;
    public event Action CurrentValChanged;
    public event Action MaxValChanged;
    #endregion

    #region event handdlers
    protected virtual void HandleMinValEvents()
    {
        MinValChanged?.Invoke();
    }
    protected virtual void HandleCurrValModEvents()
    {
        CurrentValChanged?.Invoke();
    }
    protected virtual void HandleMaxValEvents()
    {
        MaxValChanged?.Invoke();
    }
    #endregion

    public override string ToString()
    {
        return "Stat can have values from " + _minValue + " to " + _maxValue.RealValue + "; Current value modifier is " + _currnetValueMod.RealValue;
    }
}

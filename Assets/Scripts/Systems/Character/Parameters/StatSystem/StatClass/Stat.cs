using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat : CharParameterBase, IMaxValModifiable, IMinValUnmod, ICurrValModifiable 
{
    [SerializeField] private float _minValue;
    [SerializeField] private ModVar _currentValue;
    [SerializeField] private ModVar _maxValue;

    #region constructors and destructor
    public Stat(string name, float maxValue, float minValue, float currentValue) : base(name)
    {
        _name = name;
        _minValue = minValue;
        _maxValue = new ModVar(maxValue);
        _currentValue = new ModVar(currentValue);

        _maxValue.IsLowerBounded = true;
        _maxValue.LowerBound = minValue;

        _currentValue.ValueChanged += HandleCurrentValEvents;
        _maxValue.ValueChanged += HandleMaxValEvents;
    }
    public Stat(string name, float maxValue, float minValue) : this(name, maxValue, minValue, maxValue) { }
    /// <summary>
    /// Default min value is 0. Default current value is max value
    /// </summary>
    public Stat(string name, float maxValue) : this(name, DEFAULT_MIN_VALUE, maxValue, maxValue) { }
    /// <summary>
    /// Default min value is 0 and default max value is 30. Default current value is max value
    /// </summary>
    public Stat(string name) : this(name, DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE, DEFAULT_MAX_VALUE) { }
    /// <summary>
    /// Default min value is 0 and default max value is 30. Default current value is max value
    /// </summary>
    public Stat() : this(DEFAULT_NAME, DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE, DEFAULT_MAX_VALUE) { }
    ~Stat() // дивна поведінка, перевірити
    {
        _currentValue.ValueChanged -= HandleCurrentValEvents;
        _maxValue.ValueChanged -= HandleMaxValEvents;
    }
    #endregion

    #region properties
    public float MinValue
    {
        get => _minValue;
        set 
        {
            if (value > _maxValue.RealValue) _minValue= _maxValue.RealValue;
            else _minValue = value;
            MinValChangedInvoke();
        }
    }
    public float CurrentValueBase
    {
        get => _currentValue.BaseValue;
        set
        {
            if (value > _maxValue.RealValue) _currentValue.BaseValue = _maxValue.RealValue;
            else if (value < _minValue) _currentValue.BaseValue = _minValue;
            else _currentValue.BaseValue = value;
        }
    }
    public float CurrentValue { get => _currentValue.RealValue; }

    public float MaxValueBase
    {
        get => _maxValue.BaseValue;
        set
        {
            if (value < _minValue) _maxValue.BaseValue = _minValue;
            else _maxValue.BaseValue = value;
        }
    }
    public float MaxValue { get => _maxValue.RealValue; }
    #endregion

    #region modifers operations
    // Current value modifiers
    public void AddCurrentValueModifier(Modifier modifier) => _currentValue.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetCurrentValueModifiers() => _currentValue.GetModifiers();
    public IReadOnlyList<Modifier> GetCurrentValueModifiers(ModifierType modifierType) => _currentValue.GetModifiers(modifierType);
    public bool TryRemoveCurrentValueModifier(Modifier modifier) => _currentValue.TryRemoveModifier(modifier);
    public bool TryRemoveCurrentValueAllModifiersOf(object source) => _currentValue.TryRemoveAllModifiersOf(source);

    // Max value modifiers
    public void AddMaxValueModifier(Modifier modifier) => _maxValue.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetMaxValueModifiers() => _maxValue.GetModifiers();
    public IReadOnlyList<Modifier> GetMaxValueModifiers(ModifierType modifierType) => _maxValue.GetModifiers(modifierType);
    public bool TryRemoveMaxValueModifier(Modifier modifier) => _maxValue.TryRemoveModifier(modifier);
    public bool TryRemoveMaxValueAllModifiersOf(object source) => _maxValue.TryRemoveAllModifiersOf(source);
    #endregion

    #region event handlers
    private void HandleMinValEvents() 
    {
        _maxValue.LowerBound = _minValue;
    }
    private void HandleCurrentValEvents() => CurrentValChangedInvoke();
    private void HandleMaxValEvents()
    {
        if (_currentValue.BaseValue > _maxValue.RealValue)
            _currentValue.BaseValue = _maxValue.RealValue;
        MaxValChangedInvoke();
    }
    #endregion

    public override string ToString()
    {
        return "Stat can have values from " + _minValue + " to " + _maxValue.RealValue + "; Current value is " + _currentValue.RealValue;
    }
}

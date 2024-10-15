using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharResource : CharParameterBase, IMinValUnmod, ICurrValUnmod, IMaxValModifiable
{
    [SerializeField] protected float _minValue;
    [SerializeField] protected float _currentValue;
    [SerializeField] protected ModVar _maxValue;

    #region constructors and destructor
    public CharResource(string name, float maxValue, float minValue, float currentValue)
    {
        _name = name;
        _minValue = minValue;
        _maxValue = new ModVar(maxValue);
        _currentValue = currentValue;

        _maxValue.IsLowerBounded = true;
        _maxValue.LowerBound = minValue;

        _maxValue.ValueChanged += HandleMaxValEvents;
    }
    public CharResource(string name, float maxValue, float minValue) : this(name, maxValue, minValue, maxValue) { }
    /// <summary>
    /// Default min value is 0. Default current value is max value
    /// </summary>
    public CharResource(string name, float maxValue) : this(name, DEFAULT_MIN_VALUE, maxValue, maxValue) { }
    /// <summary>
    /// Default min value is 0 and default max value is 30. Default current value is max value
    /// </summary>
    public CharResource(string name) : this(name, DEFAULT_MAX_VALUE, DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE) { }
    public CharResource() : this(DEFAULT_NAME, DEFAULT_MAX_VALUE, DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE) { }
    public CharResource(CharResource charResource)
    {
        _maxValue = charResource._maxValue;
        _minValue = charResource._minValue;
        _currentValue = charResource._currentValue;
    }
    ~CharResource()
    {
        _maxValue.ValueChanged -= HandleMaxValEvents;
    }
    #endregion

    #region properties
    public float MinValue
    {
        get => _minValue;
        set
        {
            if (value > _maxValue.RealValue) _minValue = _maxValue.RealValue;
            else _minValue = value;
            MinValChanged?.Invoke();
        }
    }
    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            if (value > _maxValue.RealValue) _currentValue = _maxValue.RealValue;
            else _currentValue = value;
            CurrentValChanged?.Invoke();
        }
    }
    public float MaxValue { get => _maxValue.RealValue; }
    public float MaxValueBase
    {
        get => _maxValue.BaseValue;
        set
        {
            if (value < _minValue) _maxValue.BaseValue = _minValue;
            else _maxValue.BaseValue = value;
        }
    }
    #endregion

    #region modifers operations
    public void AddMaxValueModifier(Modifier modifier) => _maxValue.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetMaxValueModifiers() => _maxValue.GetModifiers();
    public IReadOnlyList<Modifier> GetMaxValueModifiers(ModifierType modifierType) => _maxValue.GetModifiers(modifierType);
    public bool TryRemoveMaxValueModifier(Modifier modifier) => _maxValue.TryRemoveModifier(modifier);
    public bool TryRemoveMaxValueAllModifiersOf(object source) => _maxValue.TryRemoveAllModifiersOf(source);
    #endregion

    #region external operations
    public void SubscribeToAll(Action action)
    {
        MinValChanged += action;
        CurrentValChanged += action;
        MaxValChanged += action;
    }

    public void UnsubscribeToAll(Action action)
    {
        MinValChanged -= action;
        CurrentValChanged -= action;
        MaxValChanged -= action;
    }
    #endregion

    public event Action MinValChanged;
    public event Action CurrentValChanged;
    public event Action MaxValChanged;

    protected virtual void HandleMaxValEvents()
    {
        MaxValChanged?.Invoke();
        if (_currentValue > _maxValue.RealValue)
            _currentValue = _maxValue.RealValue;
    }

    public override string ToString()
    {
        return "Stat can have values from " + _minValue + " to " + _maxValue.RealValue + "; Current value is " + _currentValue;
    }
}

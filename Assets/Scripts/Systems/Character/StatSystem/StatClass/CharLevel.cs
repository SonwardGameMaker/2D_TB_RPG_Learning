using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharLevel : CharParameterBase, IMaxValModifiable, IMinValUnmod, ICurrValUnmod
{
    protected new const string DEFAULT_NAME = "Level";

    [SerializeField] private float _minValue;
    [SerializeField] private float _currentValue;
    [SerializeField] private ModVar _maxValue;

    public CharLevel(float maxValue, float minValue, float currentValue) : base(DEFAULT_NAME)
    {
        _minValue = minValue;
        _maxValue = new ModVar(maxValue);
        _currentValue = currentValue;

        _maxValue.IsLowerBounded = true;
        _maxValue.LowerBound = minValue;

        _maxValue.ValueChanged += HandleMaxValEvents;

        _maxValue.IsLowerBounded = true;
        _maxValue.IsUpperBounded = true;
        _maxValue.LowerBound = 20;
        _maxValue.UpperBound = 20;
    }
    public CharLevel(float maxValue, float minValue) : this(maxValue, minValue, maxValue) { }
    /// <summary>
    /// Default min value is 0. Default current value is max value
    /// </summary>
    public CharLevel(float maxValue) : this(DEFAULT_MIN_VALUE, maxValue, maxValue) { }
    /// <summary>
    /// Default min value is 0 and default max value is 30. Default current value is max value
    /// </summary>
    public CharLevel() : this(DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE, DEFAULT_MAX_VALUE) { }
    ~CharLevel() // дивна поведінка, перевірити
    {
        _maxValue.ValueChanged -= HandleMaxValEvents;
    }

    public float MinValue
    {
        get => _minValue;
        set
        {
            if (value > _maxValue.RealValue) _minValue = _maxValue.RealValue;
            else _minValue = value;
            MinValChangedInvoke();
        }
    }

    public float CurrentValue 
    {
        get => _currentValue; 
        set
        {
            if (value > _maxValue.RealValue) _currentValue = _maxValue.RealValue;
            else if (value < _minValue) _currentValue = _minValue;
            else _minValue = value;
            CurrentValChangedInvoke();
        }
    }

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

    // Max value modifiers
    public void AddMaxValueModifier(Modifier modifier) => _maxValue.AddModifier(modifier);
    public IReadOnlyList<Modifier> GetMaxValueModifiers() => _maxValue.GetModifiers();
    public IReadOnlyList<Modifier> GetMaxValueModifiers(ModifierType modifierType) => _maxValue.GetModifiers(modifierType);
    public bool TryRemoveMaxValueModifier(Modifier modifier) => _maxValue.TryRemoveModifier(modifier);
    public bool TryRemoveMaxValueAllModifiersOf(object source) => _maxValue.TryRemoveAllModifiersOf(source);

    private void HandleMaxValEvents()
    {
        if (_currentValue > _maxValue.RealValue)
            _currentValue = _maxValue.RealValue;
        MaxValChangedInvoke();
    }
}

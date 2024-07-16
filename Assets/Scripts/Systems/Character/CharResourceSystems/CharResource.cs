using System;
using UnityEngine;

[Serializable]
public class CharResource : CharParameterBase, IMinValUnmod, ICurrValUnmod, IMaxValModifiable
{
    [SerializeField] protected float _minValue;
    [SerializeField] protected float _currentValue;
    [SerializeField] protected ModVar _maxValue;

    public CharResource(float maxValue, float minValue, float currentValue)
    {
        _minValue = minValue;
        _maxValue = new ModVar(maxValue);
        _currentValue = currentValue;

        _maxValue.IsLowerBounded = true;
        _maxValue.LowerBound = minValue;

        _maxValue.ValueChanged += HandleMaxValEvents;
    }
    ~CharResource() // дивна поведінка, перевірити
    {
        _maxValue.ValueChanged -= HandleMaxValEvents;
    }
    public CharResource(float maxValue, float minValue) : this(maxValue, minValue, maxValue) { }
    /// <summary>
    /// Default min value is 0. Default current value is max value
    /// </summary>
    public CharResource(float maxValue) : this(DEFAULT_MIN_VALUE, maxValue, maxValue) { }
    /// <summary>
    /// Default min value is 0 and default max value is 30. Default current value is max value
    /// </summary>
    public CharResource() : this(DEFAULT_MIN_VALUE, DEFAULT_MAX_VALUE, DEFAULT_MAX_VALUE) { }
    public float MinValue
    {
        get => _minValue;
        set
        {
            if (value > _maxValue.RealValue) _minValue = _maxValue.RealValue;
            else _minValue = value;
            MinValChanged();
        }
    }
    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            if (value > _maxValue.RealValue) _currentValue = _maxValue.RealValue;
            else if (value < _minValue) _currentValue = _minValue;
            else _currentValue = value;
            CurrentValChanged?.Invoke();
        }
    }
    public ModVar MaxValue
    {
        get => _maxValue;
        set => SetMaxValueBase(value.BaseValue);
    }
    public void SetMaxValueBase(float baseValue)
    {
        //Debug.Log("Max value changed");
        if (baseValue < _minValue) _maxValue.BaseValue = _minValue;
        else _maxValue.BaseValue = baseValue;
    }

    public void AddMaxValueModifier(Modifier modifier) => _maxValue.AddModifier(modifier);

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
